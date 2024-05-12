using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 20f;

    [SerializeField] Text countdown;

    void Start()
    {
        currentTime = startingTime;

    }
    // Update is called once per frame
    void Update()

    {
        currentTime -= 1 * Time.deltaTime;
        countdown.text = "Remaining Time: " + currentTime.ToString("0");

        if (currentTime<= 0)
        {
            currentTime = 0;
        }
    }

    //add what happens when it reaches 0
}
