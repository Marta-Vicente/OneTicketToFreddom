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

    public GameObject eventScreen;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    
    public List<GameplayEventInfo> gameplayEventInfos;

    private GameplayEventInfo _currentGamplayEvent;
    
    // How much suspicion gain is increased per week (20%)
    [SerializeField] private float sus_increase = 0.2f;
    [SerializeField] private SuspicionBar _suspicionBar;

    // How much suspicion is gained at the end of the week
    private float suspicion_gain = 0;

    private int people = 0;
    private int num_seats;

    private int week_counter = 1;
    
    private double suspicion = 0;
    [HideInInspector] public float susModifier;
    
    void Start()
    {
        num_seats = PersonManager.Instance.seatsAvailable;
        people = PersonManager.Instance.peoplePerDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Change suspicion and reset week vars
    public void WeekForwarder() {
        if (suspicion_gain > 0) 
        {
            suspicion += suspicion_gain * (1 + sus_increase * (week_counter - 1));
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
        week_counter++;
        Console.WriteLine(week_counter);
    }

    public void BoardPerson(int threatLevel) 
    {
        if (people == num_seats) { Console.WriteLine("Boarding limit exceeded"); return; }
        
        people++;

        // If threatLevel = 1, suspicion gain will be negative
        if (threatLevel < 2)
        {
            suspicion_gain -= 4;
        }
        else {
            suspicion_gain += (threatLevel - 1) * 4;
        }
        
        _suspicionBar.SetSuspicion(suspicion_gain);
    }

    // Changes the amount of seats by change amount
    public void AlterSeatsAmount(int change)
    {
        if (-change >= num_seats) { num_seats = 0; return; }
        num_seats += change;
    }

    // Changes the amount of seats by a multiplier
    public void MultiplySeatsAmount(double change)
    {
        num_seats = (int)(num_seats * change);
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
        sequence.Append(eventScreen.transform.DOScale(Vector3.zero, 0.7f).SetDelay(10f)).OnComplete(() =>
        {
            eventScreen.SetActive(false);
            Timer.Instance.runTimer = true;
        });

        PersonManager.Instance.seatsAvailable += gameplayEventInfo.availableSeatModifier;
    }
}
