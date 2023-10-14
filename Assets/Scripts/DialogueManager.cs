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
    private int index;
    private bool DialogueEnabled=false;
    private GameState stateToSendAfter;

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

    void PlayDialogue(DialogueScriptableObject[] dialogueScriptObj){
        DialoguesScriptableObject = dialogueScriptObj[0];
        DialogueEnabled=true;
        DialoguePanel.transform.GetChild(0).gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
        Time.timeScale = 0;
    }

    IEnumerator TypeLine(){
        HeroNameDialogue.text = DialoguesScriptableObject.dialogues[index].HeroName.ToString();
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
        DialogueEnabled=false;
        Time.timeScale = 1;
    }

    public void CheckStartingLevelDialogue(){
        stateToSendAfter = GameState.PlayerSelection;
        if (isValidDialogueScriptObj(ListOfStartingLevelDialogues)){
            PlayDialogue(ListOfStartingLevelDialogues);
        }
        else{
            Debug.Log(isValidDialogueScriptObj(ListOfStartingLevelDialogues));
            GameManager.Instance.UpdateGameState(stateToSendAfter);
        }
    }
    
    public void CheckEvacuationDialogue(){

    }

    public void CheckDeathDialogue(){

    }

    public void CheckEndingLevelDialogue(){

    }

    public void PlayInstantDialogue(DialogueScriptableObject[] dialogue){
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

    



}
