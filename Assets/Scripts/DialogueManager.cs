using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        DialogueChecker.checkListOfDialoguesStructure(ListOfStartingLevelDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfOnDeathDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfOnEvacuationDialogues);
        DialogueChecker.checkListOfDialoguesStructure(ListOfEndingLevelDialogues);
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
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(false);
        textComponent.text = string.Empty;
        DialogueEnabled=false;
        Time.timeScale = 1;
    }

    public void PlayDialogueOnStartingLevel(){
        stateToSendAfter = GameState.PlayerSelection;
        if (ListOfStartingLevelDialogues.Length!= 0 && ListOfStartingLevelDialogues!= null){
            for (int i = 0; i < ListOfStartingLevelDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForStartingDialogues(ListOfStartingLevelDialogues[i])){
                    PlayDialogue(ListOfStartingLevelDialogues[i]);
                    return;
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
        if (ListOfOnDeathDialogues.Length!= 0 && ListOfStartingLevelDialogues!= null){
            for (int i = 0; i < ListOfOnDeathDialogues.Length; i++){
                if (DialogueChecker.isHeroesMatchingForOnDeathDialogues(ListOfOnDeathDialogues[i], DeadHero)){
                    PlayDialogue(ListOfStartingLevelDialogues[i]);
                    return;
                }}
        }
    }

    public void PlayDialogueOnEscape(){

    }
    public void PlayDialogueOnEndingLevel(){

    }
}
