using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    [Header("Game")] 
    public int numberOfDayPerWeek = 3; 
    
    [Header("Game Events")]
    public GameObject eventScreen;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;

    [Header("Weakly Events")]
    public List<GameplayEventInfo> gameplayEventInfos;
    
    private GameplayEventInfo _currentGamplayEvent;

    // How much suspicion gain is increased per week (20%)
    [SerializeField] private float sus_increase = 0.2f;
    [SerializeField] private SuspicionBar _suspicionBar;

    // How much suspicion is gained at the end of the week
    private float suspicion_gain = 0;

    private int people = 0;
    private int numSeats;

    private int _weekCounter = 0;
    private int _dayCounter;

    private double suspicion = 0;
    [HideInInspector] public float susModifier;

    void Start()
    {
        numSeats = PersonManager.Instance.seatsAvailable;
        people = PersonManager.Instance.peoplePerDay;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Change suspicion and reset week vars
    public void WeekForwarder()
    {
        if (suspicion_gain > 0)
        {
            suspicion += suspicion_gain * (1 + sus_increase * (_weekCounter - 1));
        }
        else
        {
            suspicion += suspicion_gain;
        }

        if (suspicion < 0)
        {
            suspicion = 0;
        }
        else if (suspicion >= 100)
        {
            Console.WriteLine("You have been caught, game over");
            return;
        }

        people = 0;
        suspicion_gain = 0;
        Console.WriteLine(_weekCounter);
    }

    public void BoardPerson(int threatLevel)
    {
        if (people == numSeats)
        {
            Console.WriteLine("Boarding limit exceeded");
            return;
        }

        people++;

        // If threatLevel = 1, suspicion gain will be negative
        if (threatLevel < 2)
        {
            suspicion_gain -= 4;
        }
        else
        {
            suspicion_gain += (threatLevel - 1) * 4;
        }

        _suspicionBar.SetSuspicion(suspicion_gain);
    }

    // Changes the amount of seats by change amount
    public void AlterSeatsAmount(int change)
    {
        if (-change >= numSeats)
        {
            numSeats = 0;
            return;
        }

        numSeats += change;
    }

    // Changes the amount of seats by a multiplier
    public void MultiplySeatsAmount(double change)
    {
        numSeats = (int)(numSeats * change);
    }

    public void GameplayeEventEffect(GameplayEventInfo gameplayEventInfo)
    {
        _currentGamplayEvent = gameplayEventInfo;
        eventName.text = gameplayEventInfo.name;
        eventDescription.text = gameplayEventInfo.description;
        eventScreen.SetActive(true);
        eventScreen.transform.localScale = Vector3.zero;
        
        var sequence = DOTween.Sequence();
        sequence.Append(eventScreen.transform.DOScale(Vector3.one, 0.7f));
        sequence.Append(eventScreen.transform.DOScale(Vector3.zero, 0.7f).SetDelay(10f));
        
        PersonManager.Instance.seatsAvailable += gameplayEventInfo.availableSeatModifier;
    }

    public void NewWeek()
    {
        WeekForwarder();
        var pm = PersonManager.Instance;
        pm.consequences = new List<(string, string)>(); //restart list after end of the week
        _dayCounter = 0;
        _weekCounter++;
        GameplayeEventEffect(gameplayEventInfos[UnityEngine.Random.Range(0, gameplayEventInfos.Count)]);
    }

    public void EndWeek()
    {
        var screen = UIManager.Instance.endOfWeekScreen;
        screen.SetActive(true);

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            screen.SetActive(false);
            PersonManager.Instance.seatsAvailableCounter = PersonManager.Instance.seatsAvailable;
            NewWeek();
        }

        StartCoroutine(Delay());
    }

    public void NextDay()
    {
        _dayCounter++;
        if (_dayCounter == numberOfDayPerWeek)
        {
            EndWeek();
            return;
        }
        
        var screen = UIManager.Instance.nextDayScreen;
        screen.SetActive(true);

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            screen.SetActive(false);
            PersonManager.Instance.NewDay();
        }

        StartCoroutine(Delay());
    }
}