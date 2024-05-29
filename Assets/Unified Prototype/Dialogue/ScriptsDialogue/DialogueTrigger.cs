using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset inkJSONReplay;

    private bool playerInRange;
    private bool played;

    private void Awake()
    {
        playerInRange = false;
        played = false;
        visualCue.SetActive(false);
    }

    private void Update(){
        if(playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
            visualCue.SetActive(true);
        else
            visualCue.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player")
            playerInRange = false;
    }

    public void EnterDialogue(){
        if(playerInRange && !DialogueManager.GetInstance().makingChoices){
            //PlayerController.instance.Lock();
            if(!played){
                played = true;
                //DialogueManager.GetInstance().EnterCutsceneMode(inkJSON);
            }
            //else
                //DialogueManager.GetInstance().EnterCutsceneMode(inkJSONReplay);
        }
    }

}
