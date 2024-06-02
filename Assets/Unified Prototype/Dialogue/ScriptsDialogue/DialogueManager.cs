using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DialogueManager : MonoBehaviour
{
    enum portraitsOrder
    {
        Hero = 0,
        RedHood = 1,
        Gretel = 2,
        Wolf = 3,
        Witch = 4,
        Dragon = 5,
        Hansel = 6
    };

    public Sprite[] portraits;
    
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialogueTextBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    [Header("Choice UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying {get; private set;}
    public bool makingChoices {get; private set;}


    public float dialogueLength = 0;
        
    public string currentTag = "";
    public int narrativeTags = 0;

    //NEW
    private static DialogueManager instance;

    private void Awake(){
        if (instance != null)
            Debug.Log("More than one Dialogue Manager in scene");
        instance = this;

        dialogueTextBox.SetActive(false);
        dialogueIsPlaying = false;
    }

    public static DialogueManager GetInstance(){
        return instance;
    }

    private void Start(){
  
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (dialogueIsPlaying && Input.GetMouseButtonDown(0) && !makingChoices)
        {
            ContinueStory();
            AudioManager.instance.Stop("Type1");
            AudioManager.instance.Stop("Type2");
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if(!dialogueIsPlaying)
        {
            narrativeTags = 0;
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            //this.portrait.GetComponent<Image>().sprite = portrait;
        }
        
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode(){
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialogueTextBox.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory(){
        if(currentStory.canContinue)
        {
            dialogueLength = 3;
            string sentence = currentStory.Continue();

            if (sentence.Equals(""))
            {
                ContinueStory();
                return;
            }

            List<string> tags = currentStory.currentTags;
            
            if (tags.Any())
            {
                currentTag = tags[0];
                /*var value = (int) System.Enum.Parse(typeof(portraitsOrder), currentTag);
                portrait.sprite = portraits[value];*/
            }

            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            DisplayChoices();
        }
        else
            StartCoroutine(ExitDialogueMode());
    }

    private void DisplayChoices(){
        var currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length){
            Debug.LogError("More choices were given than the UI can support. Number if choices given: " + currentChoices.Count);
            return;
        }

        var index = 0;
        foreach(var choice in currentChoices){
            makingChoices = true;
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(var i = index; i < choices.Length; i++)
            choices[i].gameObject.SetActive(false);

        //StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice(){
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex){
        AudioManager.instance.Play("Action Button");
        Debug.Log("Make Choice");
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        makingChoices = false;
    }

    private IEnumerator TypeSentence (string sentence)
    {
        dialogueTextBox.SetActive(true);
        dialogueText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            var rng = Random.Range(0f, 1f);
            AudioManager.instance.Play(rng > 0.5 ? "Type1" : "Type2");
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
        AudioManager.instance.Play("Bell");
        yield return new WaitForSeconds(1);
    }

}
