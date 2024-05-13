using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicGameEventListener : MonoBehaviour, IBasicGameEventListener
{
    [Tooltip("Event to register with.")] 
    [SerializeField] private BasicGameEvent gameEvent;

    [Tooltip("Response to invoke when Event is raised")] 
    [SerializeField] private UnityEvent response;

    private void OnEnable()
    {
        if(gameEvent != null) gameEvent.RegisterListener(this);
    }

    public void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response?.Invoke();
    }
}
