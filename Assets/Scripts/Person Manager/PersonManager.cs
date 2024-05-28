using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//************************************************JSON************************************************

[System.Serializable]
public class PeopleArray
{
    //public PersonData[] people;
    public List<PersonData> people;
}


[System.Serializable]
public class PersonData
{
    public string characterName;
    public int age;
    public int threatLevel;
    public string threatLevelJustification;
    public int healthStatus;
    public string healthStatusDescription;
    public string description;
    public string consequenceOfAccepting;
    public string consequenceOfRejecting;
}

//************************************************PERSON MANAGER************************************************
public class PersonManager : MonoBehaviour
{
    public static PersonManager Instance;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    public Text characterText;
    
    public List<Image> charactersImageList = new List<Image>();
    public bool accepted = false;
    public bool rejected = false;

    [SerializeField] Text ticketCounter;
    [SerializeField] Text personsRemainingCounter;
    private int alreadyCheked;

    public GameObject personPrefab;     //person template
    public string peopleJsonFileName = "people.json";       //JSON with characters' info
    public int peoplePerDay;       //number of people who show up in one day
    public int seatsAvailable;     //number of seats available to give
    public int numberOfNews;     //number of news that will appear in the newspaper at the end of the week
    PeopleArray peopleData;                         //characters' info read from JSON
    
    [HideInInspector] public int seatsAvailableCounter;

    //KEYS
    [HideInInspector] public bool startOfWeek = true;
    List<Person> showedUpCharacters;
    [HideInInspector] public List<(string, string)> consequences;  //list where the correct consequences are stored for each character we face
    //KEYS 2
    private bool keyPressed = false;    

    void Start()
    {
        // Read the JSON file
        string filePath = Path.Combine(Application.dataPath + "/Scripts/Person Manager/", peopleJsonFileName);
        string jsonContent = File.ReadAllText(filePath);

        // Parse the JSON data
        peopleData = JsonUtility.FromJson<PeopleArray>(jsonContent);

        //person info
        characterText.text = " ";

        alreadyCheked = peoplePerDay;

        numberOfNews = seatsAvailable;
        seatsAvailableCounter = seatsAvailable;
        GameManager.Instance.NewWeek();
    }

    public void ChangeImageStatus(bool status,Person p)
    {
        for (int i = 0; i < charactersImageList.Count; i++)
        {
            if (charactersImageList[i].name == p.getName())
            {
                charactersImageList[i].gameObject.SetActive(status);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(seatsAvailableCounter != 0 && showedUpCharacters.Count > 0)
        {
            var viewedPerson = showedUpCharacters[0];
            ChangeImageStatus(true,viewedPerson);
            
            characterText.text = viewedPerson.Speak();
            if (rejected) //reject
            {
                Debug.Log("You rejected " + viewedPerson.characterName);
                if(!consequences.Contains((viewedPerson.characterName, viewedPerson.consequenceOfRejecting)))   //if not seen yet
                {
                    (string, string) newConsequence = (viewedPerson.characterName, viewedPerson.consequenceOfRejecting);
                    consequences.Add(newConsequence);
                }
                showedUpCharacters.RemoveAt(0);
                rejected = false;
                --alreadyCheked;
                ChangeImageStatus(false,viewedPerson);

            }
            else if (accepted)  //accept
            {
                peopleData.people = ChoosePerson(peopleData.people, viewedPerson.personData);
                seatsAvailableCounter--;
                Debug.Log("You accepted " + viewedPerson.characterName);
                if (!consequences.Contains((viewedPerson.characterName, viewedPerson.consequenceOfRejecting)))  //if not seen yet
                {
                    (string, string) newConsequence = (viewedPerson.characterName, viewedPerson.consequenceOfAccepting);
                    consequences.Add(newConsequence);
                }
                else if (consequences.Contains((viewedPerson.characterName, viewedPerson.consequenceOfRejecting)))  //if had been rejected before
                {

                    consequences.Remove((viewedPerson.characterName, viewedPerson.consequenceOfRejecting));
                    (string, string) newConsequence = (viewedPerson.characterName, viewedPerson.consequenceOfAccepting);
                    consequences.Add(newConsequence);
                }
                
                showedUpCharacters.RemoveAt(0);
                accepted = false;
                --alreadyCheked;
                ChangeImageStatus(false,viewedPerson);
            }

            if (alreadyCheked <=0)
            {
                personsRemainingCounter.text = "Remaing number of people to check : 0";
            }
            else
            {
                personsRemainingCounter.text = "Remaing number of people to check : " + alreadyCheked;

            }
        }

        ticketCounter.text = "Available Seats: " + seatsAvailableCounter.ToString();
    }
    public void AcceptClicked() 
    {
        accepted = true;
        GameManager.Instance.BoardPerson(showedUpCharacters[0].threatLevel);
        if(seatsAvailableCounter == 0)    //NEXT WEEK
        {  
            GameManager.Instance.EndWeek();
        }
        else if (showedUpCharacters.Count == 0)     //NEXT DAY
        {
            GameManager.Instance.NextDay();
        }
    }

    public void RejectClicked()
    {
        rejected = true;
        if(seatsAvailableCounter == 0)    //NEXT WEEK
        {  
            GameManager.Instance.EndWeek();
        }
        else if (showedUpCharacters.Count == 0)     //NEXT DAY
        {
            GameManager.Instance.NextDay();
        }
    }

    //choose randomly who shows up in this day
    List<Person> ShowUpCharacters(List<PersonData> peopleData)
    {
        List<PersonData> tempList = new List<PersonData>(peopleData);   //temporary list to ensure that the same person doesn't show up twice in the same day
        List<Person> showedUpList = new List<Person>();     //people who show up in the day

        for (int j = 0; j < peoplePerDay; j++)
        {
            if (tempList.Count != 0)
            {
                int randomPersonIndex = UnityEngine.Random.Range(0, tempList.Count - 1);

                GameObject newPerson = Instantiate(personPrefab, Vector3.zero, Quaternion.identity);
                Person personComponent = newPerson.GetComponent<Person>();
                PersonData personData = tempList[randomPersonIndex];

                personComponent.name = personData.characterName;
                personComponent.characterName = personData.characterName;
                personComponent.age = personData.age;
                personComponent.threatLevel = personData.threatLevel;
                personComponent.threatLevelJustification = personData.threatLevelJustification;
                personComponent.healthStatus = personData.healthStatus;
                personComponent.healthStatusDescription = personData.healthStatusDescription;
                personComponent.description = personData.description;
                personComponent.consequenceOfAccepting = personData.consequenceOfAccepting;
                personComponent.consequenceOfRejecting = personData.consequenceOfRejecting;
                personComponent.personData = personData;

                showedUpList.Add(personComponent);
                tempList.RemoveAt(randomPersonIndex);
            }

        }
        return showedUpList;
    }

    //Returns the people that will show up this day
    public void NewDay()
    {
        showedUpCharacters = ShowUpCharacters(peopleData.people);
    }

    //Remove person from character's list, once the person is accepted
    List<PersonData> ChoosePerson(List<PersonData> peopleData, PersonData chosenPerson)
    {
        peopleData.Remove(chosenPerson);
        return peopleData;
    }

    void PrintNews()
    {
        for (int i = 0; i < consequences.Count; i++)
        {
            int randConsequenceIndex = UnityEngine.Random.Range(0, consequences.Count - 1);
            (string, string) consequence = consequences[randConsequenceIndex];
            Debug.Log(consequence.Item2);
            consequences.Remove(consequence);
        }
    }
}

