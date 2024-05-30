using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

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
    
    float currentTime = 0f;
    float startingTime = 20f;
    public bool runTimer;

    [SerializeField] Text countdown;

    void Start()
    {
        currentTime = startingTime;
        runTimer = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!runTimer) return;
        
        currentTime -= 1 * Time.deltaTime;
        countdown.text = "Remaining Time: " + currentTime.ToString("0");

        if (currentTime<= 0)
        {
            currentTime = 0;
        }
    }

    public void StartTimer()
    {
        runTimer = false;
    }

    //add what happens when it reaches 0
}
