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

    public DialogueScriptableObject[] listOfDialogues = null;
    public String[] lines;
    Scene scene;
    public float textSpeed=0.3f;
    public int index;
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
        listOfDialogues = Resources.LoadAll<DialogueScriptableObject>("Dialogues/" + scene.name);
        textComponent.text=string.Empty;
    }

    void Update()
    {
        if(DialogueEnabled)
        {
            if(Input.anyKeyDown)
            {
                if (textComponent.text == listOfDialogues[0].dialogues[index].Dialogue)
                {
                    NextLine();
                }
                else{
                    StopAllCoroutines();
                    textComponent.text = listOfDialogues[0].dialogues[index].Dialogue;
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
        HeroNameDialogue.text = listOfDialogues[0].dialogues[index].HeroName.ToString();
        if (listOfDialogues[0].dialogues[index].HeroPortrait)
        {
            HeroPortraitDialogue.GetComponent<Image>().sprite=listOfDialogues[0].dialogues[index].HeroPortrait;
        }
        else{
            HeroPortraitDialogue.GetComponent<Image>().sprite = Resources.Load<HeroScriptableObject>("Heroes/" + HeroNameDialogue.text).ui_PortraitHero;
        }
        foreach (char c in listOfDialogues[0].dialogues[index].Dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < listOfDialogues[0].dialogues.Length - 1)
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
            if (listOfDialogues!=null && listOfDialogues.Length!=0){   
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
