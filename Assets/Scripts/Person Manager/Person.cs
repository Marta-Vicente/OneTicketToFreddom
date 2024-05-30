using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person : MonoBehaviour
{

    public string characterName { get; set; }
    public int age { get; set; }
    public int threatLevel { get; set; }

    public string threatLevelJustification { get; set; }
    public int healthStatus { get; set; }
    public string healthStatusDescription { get; set; }

    public string description { get; set; }

    public string consequenceOfAccepting { get; set; }
    public string consequenceOfRejecting { get; set; }

    public const string DialoguePath = "Dialogues/";

    public PersonData personData { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak()
    {
        Debug.Log(characterName);
        var ink = Resources.Load<TextAsset>(DialoguePath + characterName);
        if (ReferenceEquals (ink, null))
        {
            ink = Resources.Load<TextAsset>(DialoguePath + "Test");
        }
        Debug.Log("speaking");
        DialogueManager.GetInstance().EnterDialogueMode(ink);

        /*return "Name: "  + characterName + "\n" +
               "Age:" + age + "\n \n" +
               description + "\n \n" +
               "Threat: " + ((threatLevel == -1) ? "?" : threatLevel) + "\n" +
               ((string.IsNullOrEmpty(threatLevelJustification)) ? "" : "( " + threatLevelJustification + " )") + "\n \n" +
               "health status: " + ((healthStatus == -1) ? "?" : healthStatus) + "\n" +
               "( " + healthStatusDescription + ")";*/
    }

    public string getName()
    {
        return characterName;
    }
}
