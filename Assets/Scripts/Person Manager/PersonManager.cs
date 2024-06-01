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
    public bool criticalSaving;
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
    public List<Image> charactersImageList = new List<Image>();

    [SerializeField] private Button acceptButton, rejectButton;
    [SerializeField] private TextMeshProUGUI ticketCounter, personsRemainingText;
    private int _personsRemainingCounter;

    public GameObject personPrefab;     //person template
    public string peopleJsonFileName = "people.json";       //JSON with characters' info
    public int peoplePerDay;       //number of people who show up in one day
    private PeopleArray _peopleData;                         //characters' info read from JSON

    private bool _personSpeaking;
    private Person _currentCharacter;

    //KEYS
    private bool _accepted = false;
    private bool _rejected = false;
    private List<Person> dailyCharacters;
    public List<(string, string)> consequences;  //list where the correct consequences are stored for each character we face
    //KEYS 2
    private bool keyPressed = false;    

    void Start()
    {
        // Read the JSON file
        string filePath = Path.Combine(Application.dataPath + "/Scripts/Person Manager/", peopleJsonFileName);
        string jsonContent = File.ReadAllText(filePath);

        // Parse the JSON data
        _peopleData = JsonUtility.FromJson<PeopleArray>(jsonContent);

        _personsRemainingCounter = peoplePerDay;
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

    public void NewCharacter()
    {
        if(dailyCharacters.Count > 0)
        {
            _currentCharacter = dailyCharacters[0];
            ChangeImageStatus(true, _currentCharacter);
            _currentCharacter.Speak();
            personsRemainingText.text = "Remaining number of people to check : " + _personsRemainingCounter;
        }

        ticketCounter.text = "Available Seats: " + GameManager.Instance.seatsAvailableCounter;
    }
    
    public void AcceptClicked()
    {
        if (GameManager.Instance.seatsAvailableCounter <= 0)
            return;
        
        _accepted = true;
        GameManager.Instance.BoardPerson(dailyCharacters[0].threatLevel);

        _peopleData.people = ChoosePerson(_peopleData.people, _currentCharacter.personData);
        GameManager.Instance.seatsAvailableCounter--;
        Debug.Log("You accepted " + _currentCharacter.characterName);
        if (!consequences.Contains((_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting)))  //if not seen yet
        {
            (string, string) newConsequence = (_currentCharacter.characterName, _currentCharacter.consequenceOfAccepting);
            consequences.Add(newConsequence);
        }
        else if (consequences.Contains((_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting)))  //if had been rejected before
        {

            consequences.Remove((_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting));
            (string, string) newConsequence = (_currentCharacter.characterName, _currentCharacter.consequenceOfAccepting);
            consequences.Add(newConsequence);
        }

        dailyCharacters.RemoveAt(0);
        
        if (dailyCharacters.Count == 0)
        {
            GameManager.Instance.NextDay();
            return;
        }
        
        _accepted = false;
        _personsRemainingCounter--;
        ChangeImageStatus(false, _currentCharacter);
        NewCharacter();
        acceptButton.interactable = GameManager.Instance.seatsAvailablePerWeek > 0;
    }

    public void RejectClicked()
    {
        _rejected = true;
        Debug.Log("You rejected " + _currentCharacter.characterName);
        if(!consequences.Contains((_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting)))   //if not seen yet
        {
            (string, string) newConsequence = (_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting);
            consequences.Add(newConsequence);
        }
        dailyCharacters.RemoveAt(0);
        
        if (dailyCharacters.Count == 0)
        {
            GameManager.Instance.NextDay();
            return;
        }
        
        _rejected = false;
        _personsRemainingCounter--;
        ChangeImageStatus(false, _currentCharacter);
        NewCharacter();
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
                personComponent.criticalSaving = personData.criticalSaving;
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
        dailyCharacters = ShowUpCharacters(_peopleData.people);
        _personsRemainingCounter = peoplePerDay;
        NewCharacter();
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

