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

    public DialogueScriptableObject[] listOfDialogues;
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
    }

    void CloseDialogue()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        DialogueEnabled=false;
    }

    IEnumerator TypeLine()
    {
        HeroNameDialogue.text = listOfDialogues[0].dialogues[index].HeroName.ToString();
        if (listOfDialogues[0].dialogues[index].HeroPortrait)
        {
            HeroPortraitDialogue.GetComponent<Image>().sprite=listOfDialogues[0].dialogues[index].HeroPortrait;
        }
        else{
            HeroPortraitDialogue.GetComponent<Image>().sprite = Resources.Load<HeroScriptableObject>("Heroes/" + HeroNameDialogue.text).ui_PortraitHeroForDialogue;
        }
        foreach (char c in listOfDialogues[0].dialogues[index].Dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
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
            DialogueEnabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
            StartDialogue();
        } 
    }
}
