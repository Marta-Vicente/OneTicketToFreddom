using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent<TParameter> : ScriptableObject
{
    /*
     * This is the list of listeners that this event will notify if it is raised.
     */

    private readonly List<IGameEventListener<TParameter>> _eventListeners = new List<IGameEventListener<TParameter>>();

    public void Raise(TParameter t)
    {
        for (int i = _eventListeners.Count - 1; i >= 0; i--)
            _eventListeners[i].OnEventRaised(t);
    }

    public void RegisterListener(IGameEventListener<TParameter> eventListener)
    {
        if (!_eventListeners.Contains(eventListener))
            _eventListeners.Add(eventListener);
    }
    
    public void UnregisterListener(IGameEventListener<TParameter> eventListener)
    {
        if (_eventListeners.Contains(eventListener))
            _eventListeners.Remove(eventListener);
    }
}
