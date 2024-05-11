using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 20f;

    [SerializeField] TextMeshProUGUI countdown;

    void Start()
    {
        currentTime = startingTime;

    }
    // Update is called once per frame
    void Update()

    {
        currentTime -= 1 * Time.deltaTime;
        countdown.text = currentTime.ToString("0");

        if (currentTime<= 0)
        {
            currentTime = 0;
        }
    }

    //add what happens when it reaches 0
}
