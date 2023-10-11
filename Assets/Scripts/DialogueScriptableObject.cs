using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public HeroesManager.Hero[] requiredHeroesForDialogue;
    public DialoguesStruct[] dialogues;
}

[System.Serializable]
public struct DialoguesStruct
{
    public HeroesManager.Hero HeroName;
    public Sprite HeroPortrait;
    public string dialogueLine;
}
