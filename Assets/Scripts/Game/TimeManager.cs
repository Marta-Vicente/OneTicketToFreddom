using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    private float _timeSpent;
    [HideInInspector] public int timerState;
    public bool timerRunning { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _timeSpent = 0;
        timerState = 1;
        timerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            _timeSpent += Time.deltaTime;

            if (timerState != 3)
            {
                timerState = TimerState(_timeSpent);
            }
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void ResetTimer()
    {
        _timeSpent = 0;
        timerState = 1;
        UIManager.Instance.ChangeEyeState();
    }
    
    public int TimerState(float timeSpent) {
        int state = 1;

        switch (timeSpent)
        {
            case >= 10:
                state = 3;
                break;
            case >= 5:
                state = 2;
                break;
        }

        if (state != timerState)
        {
            UIManager.Instance.ChangeEyeState();
        }
        
        return state;
    }

    public float CurrentTime()
    {
        return _timeSpent;
    }
}
