using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI HeroNameDialogue;
    public GameObject HeroPortraitDialogue;

    public DialogueScriptableObject DialoguesScriptableObject = null;
    Scene scene;

    [SerializeField]
    [Tooltip("The bigger the value is, the longer it takes to display the text.")]
    [Range(1.0f, 10.0f)]
    private float textIntervalTime;

    private int index;
    private bool DialogueEnabled=false;

    private void Awake() {
        GameManager.OnGameStateChanged += PlayDialogue;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= PlayDialogue;
    }

    void Start()
    {
        //Retrieve the dialogues in the folder that has the same name than the current scene ("Level 1" for example)
        scene = SceneManager.GetActiveScene();
        if (DialoguesScriptableObject==null)
        {
            DialoguesScriptableObject = Resources.Load<DialogueScriptableObject>("Dialogues/" + scene.name + "/StartingDialogue");
        } 
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

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        Time.timeScale = 0;
    }

    void CloseDialogue()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        DialogueEnabled=false;
        Time.timeScale = 1;

    }

    IEnumerator TypeLine()
    {
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

    void NextLine()
    {
        if (index < DialoguesScriptableObject.dialogues.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            CloseDialogue();
            GameManager.Instance.UpdateGameState(GameState.PlayerSelection);
        }
    }

    private void PlayDialogue(GameState state)
    {
        if(state == GameState.Dialogue){
            if (DialoguesScriptableObject!=null && DialoguesScriptableObject.dialogues.Length!=0){   
                DialogueEnabled=true;
                transform.GetChild(0).gameObject.SetActive(true);
                StartDialogue();
            } 
            else{
            GameManager.Instance.UpdateGameState(GameState.PlayerSelection);
            }
        }
    }
}
