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
        var rng = Random.Range(0, 2);
        
        switch (rng)
        {
            case 0:
                DangerOnTheTracks();
                break;
            case 1:
                GenorousBenefactor();
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
        CurrentGamplayEvent = ("Genorous Benefactor",
            "A genorous benefactor wants to help the cause. They have decided to donate a carriage to help with the effort.<br>Effect:<br>Available seats increase by +1");
        GameManager.Instance.AlterSeatsAmount(1);
        UIManager.Instance.ShowWeaklyEvent(CurrentGamplayEvent);
    }
}