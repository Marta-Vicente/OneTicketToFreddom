using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class GameEventListener<TParameter> : MonoBehaviour, IGameEventListener<TParameter>
{
    [Tooltip("Event to register with.")] 
    [SerializeField] private GameEvent<TParameter> gameEvent;

    [Tooltip("Response to invoke when Event is raised")] 
    [SerializeField] private UnityEvent<TParameter> response;

    private void OnEnable()
    {
        if(gameEvent != null) gameEvent.RegisterListener(this);
    }

    public void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(TParameter t)
    {
        response?.Invoke(t);
    }
}
