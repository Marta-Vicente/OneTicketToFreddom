using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;

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

    public GameObject nextDayScreen, endOfWeekScreen;
    
    [Header("Weakly Events")]
    public GameObject eventScreen;
    public TextMeshProUGUI eventName;
    public TextMeshProUGUI eventDescription;
    
    [Header("Consequences UI")]
    public GameObject consequencesScreen;
    public GameObject consequencesTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWeaklyEvent((string,string) info)
    {
        eventName.text = info.Item1;
        eventDescription.text = info.Item2;
        eventScreen.SetActive(true);
        eventScreen.transform.localScale = Vector3.zero;
        
        var sequence = DOTween.Sequence();
        sequence.Append(eventScreen.transform.DOScale(Vector3.one, 0.7f));
    }

    public void ShowConsequences(List<(string, string)> consequences)
    {
        foreach (var consequence in consequences)
        {
            var con = Instantiate(consequencesTextPrefab, consequencesScreen.transform);
            var text = con.GetComponent<TextMeshProUGUI>();
            text.text = consequence.Item2;
        }
        
        Canvas.ForceUpdateCanvases();
        consequencesScreen.SetActive(true);
        
        var sequence = DOTween.Sequence();
        sequence.Append(consequencesScreen.transform.DOScale(Vector3.one, 0.7f));
    }

    public void CleanUpConsequencesScreen()
    {
        for (int i = 0; i < consequencesScreen.transform.childCount; i++)
        {
            Destroy(consequencesScreen.transform.GetChild(i));
        }
    }
}
