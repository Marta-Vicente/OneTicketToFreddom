using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    public int seatsAvailablePerWeek;

    // How much suspicion gain is increased per week (20%)
    [SerializeField] private float sus_increase = 0.2f;
    [SerializeField] private SuspicionBar _suspicionBar;

    // How much suspicion is gained at the end of the week
    private float suspicion_gain = 0;

    private int people = 0;
    [HideInInspector] public int seatsAvailableCounter;

    private int _weekCounter = 0;
    private int _dayCounter;

    private float suspicion = 0;
    [HideInInspector] public float susModifier;

    // Highscores
    int numberSaved = 0;
    int highPrioSaved = 0;
    int highPrioBuffer = 0;

    void Start()
    {
        seatsAvailableCounter = seatsAvailablePerWeek;
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

        highPrioSaved += highPrioBuffer;
        numberSaved += people;

        _suspicionBar.SetSuspicion(suspicion);
        people = 0;
        highPrioBuffer = 0;
        suspicion_gain = 0;
        Console.WriteLine(_weekCounter);
    }

    public void BoardPerson(int threatLevel, bool highPrio)
    {
        if (people == seatsAvailableCounter)
        {
            Console.WriteLine("Boarding limit exceeded");
            return;
        }

        seatsAvailableCounter--;
        people++;
        if (highPrio) {
            highPrioBuffer++;
        }

        // If threatLevel = 1, suspicion gain will be negative
        if (threatLevel < 2)
        {
            suspicion_gain -= 4;
        }
        else
        {
            suspicion_gain += (threatLevel - 1) * 2.5f;
        }
    }

    // Changes the amount of seats by change amount
    public void AlterSeatsAmount(int change)
    {
        if (-change >= seatsAvailableCounter)
        {
            seatsAvailableCounter = 0;
            return;
        }

        seatsAvailableCounter += change;
    }

    // Changes the amount of seats by a multiplier
    public void MultiplySeatsAmount(double change)
    {
        seatsAvailableCounter = (int)(seatsAvailableCounter * change);
    }

    public void NewWeek()
    {
        WeekForwarder();
        var pm = PersonManager.Instance;
        pm.consequences = new List<(string, string)>(); //restart list after end of the week
        _dayCounter = 0;
        _weekCounter++;
        WeeklyEventManager.Instance.SelectRandomEvent();
        if (_dayCounter > 1)
        {
            pm.NewDay();
        }
    }

    public void EndWeek()
    {
        var screen = UIManager.Instance.endOfWeekScreen;
        screen.SetActive(true);

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3f);
            screen.SetActive(false);
            seatsAvailableCounter = seatsAvailablePerWeek;
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