using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public HeroesManager.Hero[] requiredHeroesForDialogue;
    public DialoguesStruct[] dialogues;
    [Space]
    [Header("Only to display the dialogue on death")]
    public bool triggerDialogueOnDeath;
    public HeroesManager.Hero deadHero = HeroesManager.Hero.NONE;
    [HideInInspector]
    public bool isStructurallyOk = true;
    [HideInInspector]
    public bool alreadyPlayed = false;
}

[System.Serializable]
public struct DialoguesStruct
{
    public HeroesManager.Hero HeroName;
    public Sprite HeroPortrait;
    public string dialogueLine;
}
