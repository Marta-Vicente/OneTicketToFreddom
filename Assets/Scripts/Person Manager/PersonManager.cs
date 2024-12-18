using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;


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
    
    [SerializeField] private TextMeshProUGUI ticketCounter, personsRemainingText, dayCounter;
    private int _personsRemainingCounter;

    public GameObject personPrefab;     //person template
    public string peopleJsonFileName = "people";       //JSON with characters' info
    public int peoplePerDay;       //number of people who show up in one day
    private PeopleArray _peopleData;                         //characters' info read from JSON

    private bool _personSpeaking;
    private Person _currentCharacter;

    //KEYS
    private List<Person> dailyCharacters;
    public List<(string, string)> consequences;  //list where the correct consequences are stored for each character we face

    void Start()
    {
        // Read the JSON file
        //string filePath = Path.Combine(Application.dataPath + "/Scripts/Person Manager/", peopleJsonFileName);
        var jsonContent = Resources.Load<TextAsset>("People/" + peopleJsonFileName);

        // Parse the JSON data
        _peopleData = JsonUtility.FromJson<PeopleArray>(jsonContent.text);

        _personsRemainingCounter = peoplePerDay;
        GameManager.Instance.NewWeek();
    }

    private void ChangeImageStatus(bool status,Person p)
    {
        foreach (var t in charactersImageList)
        {
            if (t.name == p.getName())
            {
                t.gameObject.SetActive(status);
                return;
            }
        }
    }

    public void NewCharacter()
    {
        if (dailyCharacters.Count > 0)
        {
            _currentCharacter = dailyCharacters[0];
            ChangeImageStatus(true, _currentCharacter);
            _currentCharacter.Speak();
            personsRemainingText.text = "Remaining number of people to check : " + _personsRemainingCounter;
            TimeManager.Instance.ResetTimer();
            TimeManager.Instance.StartTimer();
        }

        ticketCounter.text = "Available Seats: " + GameManager.Instance.seatsAvailableCounter;
        dayCounter.text = "Day left in the week:" + (GameManager.Instance.numberOfDayPerWeek - GameManager.Instance._dayCounter);
    }

    public void AcceptClicked()
    {
        if (GameManager.Instance.seatsAvailableCounter <= 0)
            return;


        LogManager.Instance.AddToLine(_currentCharacter.characterName + " was accepted after " + TimeManager.Instance.CurrentTime() + " seconds. There were " + GameManager.Instance.seatsAvailableCounter + " seats left.");

        GameManager.Instance.BoardPerson(dailyCharacters[0].threatLevel, dailyCharacters[0].criticalSaving);

        _peopleData.people = ChoosePerson(_peopleData.people, _currentCharacter.personData);
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
        
        
        _personsRemainingCounter--;
        ChangeImageStatus(false, _currentCharacter);
        
        if (dailyCharacters.Count == 0)
        {
            GameManager.Instance.NextDay();
            return;
        }
        
        NewCharacter();
    }

    public void RejectClicked()
    {
        Debug.Log("You rejected " + _currentCharacter.characterName);
        
        LogManager.Instance.AddToLine(_currentCharacter.characterName + " was rejected after " + TimeManager.Instance.CurrentTime() + " seconds. There were " + GameManager.Instance.seatsAvailableCounter + " seats left.");
        LogManager.Instance.WriteOut();

        if (!consequences.Contains((_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting)))   //if not seen yet
        {
            (string, string) newConsequence = (_currentCharacter.characterName, _currentCharacter.consequenceOfRejecting);
            consequences.Add(newConsequence);
        }
        dailyCharacters.RemoveAt(0);
        
        _personsRemainingCounter--;
        ChangeImageStatus(false, _currentCharacter);
        
        if (dailyCharacters.Count == 0)
        {
            GameManager.Instance.NextDay();
            return;
        }
        
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
                int randomPersonIndex = UnityEngine.Random.Range(0, tempList.Count);

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

        LogManager.Instance.AddToLine("New Day.");
        LogManager.Instance.WriteOut();        

        NewCharacter();
    }

    public void AlterPeopleAmount(int change)
    {
        if (-change >= peoplePerDay)
        {
            peoplePerDay = 0;
            return;
        }

        peoplePerDay += change;
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

