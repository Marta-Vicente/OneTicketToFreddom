using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    public Text text;

    public GameObject counter;


    public List<Image> charactersImageList = new List<Image>();
    public bool accepted = false;
    public bool rejected = false;

    [SerializeField] Text ticketCounter;
    [SerializeField] Text howmanyRemainingCounter;
    private int alreadyCheked;

    public GameObject personPrefab;     //person template
    public string peopleJsonFileName = "people.json";       //JSON with characters' info
    [SerializeField] public int peoplePerDay;       //number of people who show up in one day
    [SerializeField] public int seatsAvailable;     //number of seats available to give
    PeopleArray peopleData;                         //characters' info read from JSON

    //KEYS
    private bool executedAlready = false;
    List<Person> showedUpCharacters;
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
        text.text = " ";

        alreadyCheked = peoplePerDay;
    }

    public void changeImageStatus(bool status,Person p)
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
        Person viewedPerson;

        //counter = GameObject.Find("CountDown");

        if (!executedAlready)
        {
            showedUpCharacters = NewDay();
            executedAlready = true;
        }

        if(seatsAvailable != 0 && showedUpCharacters.Count > 0)
        {
            viewedPerson = showedUpCharacters[0];
            changeImageStatus(true,viewedPerson);
            
            text.text = viewedPerson.Speak();
            if (rejected) //reject
            {
                Debug.Log("You rejected " + viewedPerson.characterName);
                showedUpCharacters.RemoveAt(0);
                rejected = false;
                --alreadyCheked;
                changeImageStatus(false,viewedPerson);

            }
            else if (accepted)  //accept
            {
                peopleData.people = ChoosePerson(peopleData.people, viewedPerson.personData);
                seatsAvailable--;
                Debug.Log("You accepted " + viewedPerson.characterName);
                showedUpCharacters.RemoveAt(0);
                accepted = false;
                --alreadyCheked;
                changeImageStatus(false,viewedPerson);

            }

            if (alreadyCheked <=0)
            {
                howmanyRemainingCounter.text = "Remaing number of people to check : 0";
            }
            else
            {
                howmanyRemainingCounter.text = "Remaing number of people to check : " + alreadyCheked;

            }

        }
        else if(seatsAvailable == 0)
        {
            Debug.Log("No more seats available");   //NEXT WEEK
            EndWeek();
        }
        else if (showedUpCharacters.Count == 0)
        {
            executedAlready = false; //NEXT DAY
            SceneManager.LoadScene(3);
            //Debug.Log("You have seen everyone today");
        }

        ticketCounter.text = "Available Seats: " + seatsAvailable.ToString();
    }
    public void AcceptClicked() 
    {
        accepted = true;
        GameVars.Instance.BoardPerson(showedUpCharacters[0].threatLevel);
    }

    public void RejectClicked()
    {
        rejected = true;
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
                int randomPersonIndex = Random.Range(0, tempList.Count - 1);

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
                personComponent.personData = personData;

                showedUpList.Add(personComponent);
                tempList.RemoveAt(randomPersonIndex);
            }

        }
        return showedUpList;
    }

    //Returns the people that will show up this day
    List<Person> NewDay()
    {
        var gm = GameManager.Instance;
        gm.GameplayeEventEffect(gm.gameplayEventInfos[Random.Range(0, gm.gameplayEventInfos.Count)]);
        showedUpCharacters = ShowUpCharacters(peopleData.people);
        return showedUpCharacters;
    }

    //Remove person from character's list, once the person is accepted
    List<PersonData> ChoosePerson(List<PersonData> peopleData, PersonData chosenPerson)
    {
        peopleData.Remove(chosenPerson);
        return peopleData;
    }

    //when there are no more seats available or the week actually ends
    void EndWeek()
    {
        SceneManager.LoadScene(2);
        Debug.Log("The week is over");
    }
}

