using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public HeroesManager.Hero[] requiredHeroesForDialogue;
    public Dialogues[] dialogues;
}

[System.Serializable]
public struct Dialogues
{
    public HeroesManager.Hero HeroName;
    public Sprite HeroPortrait;
    public string Dialogue;
}
