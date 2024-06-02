using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeColor : MonoBehaviour
{
    [SerializeField] public Image fill;

    public void ChangeColorToNextState()
    {
        Color color;
        var state = TimeManager.Instance.timerState;

        switch (state)
        {
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.red;
                break;
            default:
                color = Color.green;
                break;
        }

        fill.color = color;
    }
}
