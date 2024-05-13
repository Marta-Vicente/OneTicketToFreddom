using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuspicionBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxSuspicion()
    {
        slider.maxValue = 100;
        fill.color = gradient.Evaluate(0f);
    }


    
    public void SetSuspicion(int suspicion)
    {
        slider.value = suspicion;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    //implement suspicion coed here and call update() to check if it is necessery to change value
}
