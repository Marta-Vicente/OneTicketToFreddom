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
