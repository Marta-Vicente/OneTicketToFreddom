using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyEventManager : MonoBehaviour
{
    public static WeeklyEventManager Instance;

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
    
    [HideInInspector] public (string, string) CurrentGamplayEvent;

    public void SelectRandomEvent()
    {
        var rng = Random.Range(0, 6);
        
        switch (rng)
        {
            case 0:
                DangerOnTheTracks();
                break;
            case 1:
                GenorousBenefactor();
                break;
            case 2:
                SuspicionRises();
                break;
            case 3:
                StateHoliday();
                break;
            case 4:
                RoadsConstruction();
                break;
            case 5:
                IncreasedInstability();
                break;
        }
    }

    private void DangerOnTheTracks()
    {
        CurrentGamplayEvent = ("Danger on the Tracks!",
            "Due to extreme weather conditions several carriages have been put ou of commission.<br>Effect:<br>Available seats decrease by -1");
        GameManager.Instance.AlterSeatsAmount(-1);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }
    
    private void GenorousBenefactor()
    {
        CurrentGamplayEvent = ("Generous Benefactor",
            "A generous benefactor wants to help the cause. They have decided to donate a carriage to help with the effort.<br>Effect:<br>Available seats increase by +1");
        GameManager.Instance.AlterSeatsAmount(1);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }

    private void SuspicionRises()
    {
        CurrentGamplayEvent = ("Suspicion Rises",
            "A set of revolts around Tenebris have put authorities on high alert. Policemen have been dispatched everywhere.<br>Effect:<br>Suspicion gain is doubled");
        GameManager.Instance.SetSusModifier(2);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }

    private void StateHoliday()
    {
        CurrentGamplayEvent = ("State Holiday",
            "This week is a state holiday. Policemen are nowhere to be seen. <br>Effect:<br>Suspicion will be accumulated half as fast this week");
        GameManager.Instance.SetSusModifier(0.5f);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }

    private void RoadsConstruction()
    {
        CurrentGamplayEvent = ("Roads Under Construction",
            "Roads connecting to the station are under construction. Fewer people will arrive at the station. <br>Effect:<br>Number of people per day reduced by 1");
        PersonManager.Instance.AlterPeopleAmount(-1);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }

    private void IncreasedInstability()
    {
        CurrentGamplayEvent = ("Instability on the Rise!",
            "Instability is rising throughout Tenebris. The desire to depart to Aethel has never been higher. <br>Effect:<br>Number of people per day increased by 2");
        PersonManager.Instance.AlterPeopleAmount(2);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }
}