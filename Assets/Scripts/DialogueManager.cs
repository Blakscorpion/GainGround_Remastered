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
    private int dialogueIndex=0;
    private bool dialogueChosen=false;
    private bool DialogueEnabled=false;
    private GameState stateToSendAfter = GameState.PlayMode;
    private int numberOfProtagonists;

    DialogueScriptableObject[] ListOfStartingLevelDialogues;
    DialogueScriptableObject[] ListOfOnDeathDialogues;
    DialogueScriptableObject[] ListOfOnEvacuationDialogues;
    DialogueScriptableObject[] ListOfInstantDialogues;
    DialogueScriptableObject[] ListOfEndingLevelDialogues;
    DialogueScriptableObject DialoguesScriptableObject;

    void Awake()
    {
        Instance=this;
        scene = SceneManager.GetActiveScene();
        ListOfStartingLevelDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/StartingLevelDialogues");
        ListOfOnDeathDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/DeathDialogues");
        ListOfOnEvacuationDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/EvacuationDialogues");
        ListOfInstantDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/StaticDialogues");
        ListOfEndingLevelDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name + "/EndingLevelDialogues");
        textComponent.text=string.Empty;
    }

    void Update()
    {
        if(DialogueEnabled)
        {
            if(Input.anyKeyDown)
            {
                if (textComponent.text == DialoguesScriptableObject.dialogues[index].dialogueLine)
                {
                    NextLine();
                }
                else{
                    StopAllCoroutines();
                    textComponent.text = DialoguesScriptableObject.dialogues[index].dialogueLine;
                }
            }
        }
    }

    void PlayDialogue(DialogueScriptableObject dialogueScriptObj){
        DialoguesScriptableObject = dialogueScriptObj; 
        DialogueEnabled=true;
        index = 0;
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(TypeLine());
        Time.timeScale = 0;
    }

    IEnumerator TypeLine(){
        if (DialoguesScriptableObject.dialogues[index].HeroName.ToString()==HeroesManager.Hero.CURRENT.ToString())
        {
            HeroNameDialogue.text = HeroesManager.Instance.CurrentHero.ToString();
        }
        else{
            HeroNameDialogue.text = DialoguesScriptableObject.dialogues[index].HeroName.ToString();
        }
        if (DialoguesScriptableObject.dialogues[index].HeroPortrait)
        {
            HeroPortraitDialogue.GetComponent<Image>().sprite=DialoguesScriptableObject.dialogues[index].HeroPortrait;
        }
        else{
            HeroPortraitDialogue.GetComponent<Image>().sprite = Resources.Load<HeroScriptableObject>("Heroes/" + HeroNameDialogue.text).ui_PortraitHero;
        }
        foreach (char c in DialoguesScriptableObject.dialogues[index].dialogueLine.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textIntervalTime/100);
        }
    }
    
    void NextLine(){
        if (index < DialoguesScriptableObject.dialogues.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            CloseDialogue();
            GameManager.Instance.UpdateGameState(stateToSendAfter);
        }
    }

    void CloseDialogue(){
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(false);
        textComponent.text = string.Empty;
        DialogueEnabled=false;
        Time.timeScale = 1;
    }

    public void CheckStartingLevelDialogue(){
        stateToSendAfter = GameState.PlayerSelection;
        if (isValidDialogueScriptObj(ListOfStartingLevelDialogues)){
            PlayDialogue(ListOfStartingLevelDialogues[0]);
        }
        else{
            Debug.Log(isValidDialogueScriptObj(ListOfStartingLevelDialogues));
            GameManager.Instance.UpdateGameState(stateToSendAfter);
        }
    }
    
    public void PlayEvacuationDialogue(){

    }

    public void CheckDeathDialogue(HeroesManager.Hero DeadHero){
        dialogueIndex=0;
        dialogueChosen=false;
        bool checkMatchDeadHeroe = false;
        foreach (DialogueScriptableObject dialogue in ListOfOnDeathDialogues)
        {
            checkMatchDeadHeroe=false;
            if (dialogue != null){
                dialogueChosen=true;
                for(int i = 0; i<dialogue.dialogues.Length; i+=1)
                {
                    if (dialogue.dialogues[i].HeroName == DeadHero)
                    {
                        checkMatchDeadHeroe=true;
                    }
                } 
                if (checkMatchDeadHeroe == false && dialogueChosen==true){
                        PlayDialogue(ListOfOnDeathDialogues[dialogueIndex]);
                        return;
                }
            }
            dialogueIndex++;
        }
    }

    public void PlayEndingLevelDialogue(){

    }

    public void PlayInstantDialogue(DialogueScriptableObject dialogue){
        stateToSendAfter = GameState.PlayMode;
        if (isValidDialogueScriptObj(dialogue)){
            PlayDialogue(dialogue);
        }
        else{
            GameManager.Instance.UpdateGameState(stateToSendAfter);
        }
    }

    private bool isValidDialogueScriptObj(DialogueScriptableObject[] dialogueToAnalyse){
        if (dialogueToAnalyse!= null && dialogueToAnalyse.Length!=0 && dialogueToAnalyse[0].dialogues.Length!=0){
        return true;
        }
        return false;
    }

    private bool isValidDialogueScriptObj(DialogueScriptableObject dialogueToAnalyse){
        if (dialogueToAnalyse!= null && dialogueToAnalyse.dialogues.Length!=0){
        return true;
        }
        return false;
    }

    



}
