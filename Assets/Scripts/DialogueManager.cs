using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.ExceptionServices;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    Scene scene;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI HeroNameDialogue;
    public GameObject HeroPortraitDialogue;
    public GameObject DialoguePanel;

    [SerializeField]
    [Tooltip("The bigger the value is, the longer it takes to display the text.")]
    [Range(1.0f, 10.0f)]
    private float textIntervalTime;
    private int index=0;
    private bool DialogueEnabled=false;
    private GameState stateToSendAfter = GameState.PlayMode;
    private bool firstDialogue=false;

    DialogueScriptableObject[] ListOfStartingLevelDialogues;
    DialogueScriptableObject[] ListOfOnDeathDialogues;
    DialogueScriptableObject[] ListOfOnEvacuationDialogues;
    DialogueScriptableObject[] ListOfInstantDialogues;
    DialogueScriptableObject[] ListOfEndingLevelDialogues;
    DialogueScriptableObject DialoguesScriptableObject;

    void Awake()
    {
        Instance=this;
        textComponent.text=string.Empty;
        scene = SceneManager.GetActiveScene();
        
        ListOfStartingLevelDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/StartingLevelDialogues");
        ListOfOnDeathDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/DeathDialogues");
        ListOfOnEvacuationDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/EvacuationDialogues");
        ListOfEndingLevelDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/EndingLevelDialogues");
        ListOfInstantDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/StaticDialogues");
        DialogueChecker.checkListOfDialoguesStructure(ListOfStartingLevelDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfOnDeathDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfOnEvacuationDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfEndingLevelDialogues);
    }

    void OnDisable(){
        ResetDialogues();
    }

    void Update(){
        if(DialogueEnabled){
            if(Input.anyKeyDown){
                if (textComponent.text == DialoguesScriptableObject.dialogues[index].dialogueLine){
                    NextLine();
                }
                else{
                    StopAllCoroutines();
                    textComponent.text = DialoguesScriptableObject.dialogues[index].dialogueLine;
                }}}
    }

    void PlayDialogue(DialogueScriptableObject dialogueScriptObj){
        DialoguesScriptableObject = dialogueScriptObj; 
        DialogueEnabled=true;
        firstDialogue=true;
        index = 0;
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(TypeLine());
        Time.timeScale = 0;
        dialogueScriptObj.alreadyPlayed=true;
    }

    IEnumerator TypeLine(){
        HeroNameDialogue.text = DialoguesScriptableObject.dialogues[index].HeroName.ToString();
        if (DialoguesScriptableObject.dialogues[index].HeroPortrait){
            HeroPortraitDialogue.GetComponent<Image>().sprite=DialoguesScriptableObject.dialogues[index].HeroPortrait;
        }
        else{
            HeroPortraitDialogue.GetComponent<Image>().sprite = Resources.Load<HeroScriptableObject>("Heroes/" + HeroNameDialogue.text).ui_PortraitHero;
        }
        foreach (char c in DialoguesScriptableObject.dialogues[index].dialogueLine.ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textIntervalTime/100);
        }
    }
    
    void NextLine(){
        if (index < DialoguesScriptableObject.dialogues.Length - 1){
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            CloseDialogue();
            GameManager.Instance.UpdateGameState(stateToSendAfter);
            stateToSendAfter = GameState.PlayMode;
        }
    }

    void CloseDialogue(){
        if (firstDialogue) {waitForSeconds(1f); firstDialogue=false;}
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(false);
        textComponent.text = string.Empty;
        DialogueEnabled=false;
        Time.timeScale = 1;
    }

    public void PlayDialogueOnStartingLevel(){
        stateToSendAfter = GameState.PlayerSelection;
        if (ListOfStartingLevelDialogues!= null && ListOfStartingLevelDialogues.Length!= 0){
            for (int i = 0; i < ListOfStartingLevelDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForStartingDialogues(ListOfStartingLevelDialogues[i])){
                    PlayDialogue(ListOfStartingLevelDialogues[i]);
                }}
        }
        GameManager.Instance.UpdateGameState(stateToSendAfter);
        stateToSendAfter = GameState.PlayMode;
    }

    public void PlayInstantDialogue(DialogueScriptableObject dialogue){
        stateToSendAfter = GameState.PlayMode;
        DialogueChecker.checkSingleDialogueStructure(dialogue);
        if (DialogueChecker.isHeroesMatchingForStartingDialogues(dialogue)){
            PlayDialogue(dialogue);
        }
        else{
            GameManager.Instance.UpdateGameState(stateToSendAfter);
        }
    }

    public void PlayDialogueOnDeath(HeroesManager.Hero DeadHero){
        if (ListOfOnDeathDialogues!= null && ListOfOnDeathDialogues.Length!= 0){
            for (int i = 0; i < ListOfOnDeathDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForOnDeathDialogues(ListOfOnDeathDialogues[i], DeadHero)){
                    PlayDialogue(ListOfOnDeathDialogues[i]);
                    return;
                }}
        }
    }

    public void PlayDialogueOnEscape(){
        if (ListOfOnEvacuationDialogues!= null && ListOfOnEvacuationDialogues.Length!= 0){
            for (int i = 0; i < ListOfOnEvacuationDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForEvacuatingDialogues(ListOfOnEvacuationDialogues[i])){
                    Debug.Log("Playing Dialogue NUMBER : " + i+1);
                    PlayDialogue(ListOfOnEvacuationDialogues[i]);
                }}
        }
    }
    public void PlayDialogueOnEndingLevel(){
        if (ListOfEndingLevelDialogues!= null && ListOfEndingLevelDialogues.Length!= 0){
            for (int i = 0; i < ListOfEndingLevelDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForEndingDialogues()){
                    PlayDialogue(ListOfEndingLevelDialogues[i]);
                }}
        }
    }

    IEnumerator waitForSeconds(float timeToWait){
    yield return new WaitForSeconds(timeToWait);
}

    public void ResetDialogues(){
        foreach (DialogueScriptableObject dialogue in ListOfStartingLevelDialogues){
            dialogue.alreadyPlayed = false;}
        foreach (DialogueScriptableObject dialogue in ListOfOnDeathDialogues){
            dialogue.alreadyPlayed = false;}
        foreach (DialogueScriptableObject dialogue in ListOfOnEvacuationDialogues){
            dialogue.alreadyPlayed = false;}
        foreach (DialogueScriptableObject dialogue in ListOfEndingLevelDialogues){
            dialogue.alreadyPlayed = false;}
        foreach (DialogueScriptableObject dialogue in ListOfInstantDialogues){
            dialogue.alreadyPlayed = false;}
    }
}
