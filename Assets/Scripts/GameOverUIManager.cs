using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI score;
    void Start()
    {
        var peopleSaved = PlayerPrefs.GetInt("PeopleSave", 0);
        var highPrioSaved = PlayerPrefs.GetInt("HighPrioSave", 0);

        score.text = "People saved: " + peopleSaved + "\n High-priority people saved: " + highPrioSaved;
    }
}
