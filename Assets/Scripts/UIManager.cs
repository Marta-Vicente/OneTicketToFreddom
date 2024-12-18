using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public Button acceptButton, rejectButton;
    
    [Header("Suspicion Eye")]
    public Animator eye;
    
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
        int shownConsequences = 4;
        int randConsequenceIndex;

        var con = Instantiate(consequencesTextPrefab, consequencesScreen.transform);
        var text = con.GetComponent<TextMeshProUGUI>();
        text.text = "<font=\"ThaleahFat_TTF SDF\"><mark=#000000>People saved: " + GameManager.Instance.numberSaved + "\n High-priority people saved: " + GameManager.Instance.highPrioSaved;

        var consequenceIndexes = new List<int>();
        
        for(int i=0; i<shownConsequences; i++)
        {
            randConsequenceIndex = Random.Range(0, consequences.Count - 1);
            var consequence = consequences[randConsequenceIndex];
            consequences.RemoveAt(randConsequenceIndex);
            con = Instantiate(consequencesTextPrefab, consequencesScreen.transform);
            text = con.GetComponent<TextMeshProUGUI>();
            text.text = "<font=\"ThaleahFat_TTF SDF\"><mark=#000000>";
            text.text += consequence.Item2;
        }
        /*foreach (var consequence in consequences)
        {
            con = Instantiate(consequencesTextPrefab, consequencesScreen.transform);
            text = con.GetComponent<TextMeshProUGUI>();
            text.text = consequence.Item2;
        }*/
        
        Canvas.ForceUpdateCanvases();
        consequencesScreen.SetActive(true);
        
        var sequence = DOTween.Sequence();
        sequence.Append(consequencesScreen.transform.DOScale(Vector3.one, 0.7f));
    }

    public void CleanUpConsequencesScreen()
    {
        for (int i = 0; i < consequencesScreen.transform.childCount; i++)
        {
            var con = consequencesScreen.transform.GetChild(i);
            Destroy(con.gameObject);
        }
    }

    public void ChangeEyeState()
    {
        eye.SetTrigger("CloseEye");
    }
}
