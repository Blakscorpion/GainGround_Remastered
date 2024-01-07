using System;
using UnityEngine;
[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroScriptableObject : ScriptableObject
{
    [Header("Hero Settings")]
    public HeroesManager.Hero HeroName;
    public string HeroDescription;
    public int health;
    public int speed;
    public Sprite HeroSprite;
    
    [Header("Primary Weapon Settings")]
    public GameObject PrimaryAmmoPrefab;
    public int PrimaryAmmoDamage;
    public int PrimaryAmmoTimeInterval;
    public Sprite UIWeaponIcon1;
    public string primaryWeaponDescription;

    [Header("Secondary Weapon Settings")]
    public GameObject SecondaryAmmoPrefab;
    public int SecondaryAmmoDamage;
    public int SecondaryAmmoTimeInterval;
    public Sprite UIWeaponIcon2;
    public string secondaryWeaponDescription;


    [Header("Hero Images for Dialogues")]
    public Sprite UIPortraitForDialogue_Default;
    public Sprite UIPortraitForDialogue_Angry;
    public Sprite UIPortraitForDialogue_Astonished;
    public Sprite UIPortraitForDialogue_Fear;
    public Sprite UIPortraitForDialogue_Laugh;
    
    [Header("Hero Images for Selection Screen")]
    public Sprite UIPortraitForSelectionScreen;
    public Sprite UIPortraitForNextSelection;

    [Header("Hero Animation")]
    public Animator HeroAnimator;
    public Animation HeroAnimation;


}
