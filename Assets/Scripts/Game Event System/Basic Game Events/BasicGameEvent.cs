using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicGameEvent", menuName = "Events/Basic Game Event")]
public class BasicGameEvent : ScriptableObject
{
    /*
     * This is the list of listeners that this event will notify if it is raised.
     */

    private readonly List<IBasicGameEventListener> _eventListeners = new List<IBasicGameEventListener>();

    public void Raise()
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised();
    }

    public void RegisterListener(BasicGameEventListener eventListener)
    {
        if (!_eventListeners.Contains(eventListener))
            _eventListeners.Add(eventListener);
    }
    
    public void UnregisterListener(IBasicGameEventListener eventListener)
    {
        if (_eventListeners.Contains(eventListener))
            _eventListeners.Remove(eventListener);
    }
}
