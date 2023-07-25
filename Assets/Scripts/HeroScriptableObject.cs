using UnityEngine;
[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class HeroScriptableObject : ScriptableObject
{
    public Animator HeroAnimator;
    public Animation HeroAnimation;
    public HeroesManager.Hero HeroName;
    public string HeroDescription;
    public Sprite HeroSprite;
    public int health;
    public int speed;
    
    public GameObject PrimaryAmmo;
    public int PrimaryAmmoDamage;
    public int PrimaryAmmoTimeInterval;
    
    public GameObject SecondaryAmmo;
    public int SecondaryAmmoDamage;
    public int SecondaryAmmoTimeInterval;

    public Sprite ui_PortraitHero; 
    public Sprite ui_AvatarPortraitForDialogue;
    public Sprite ui_ImageHeroForSelectionScreen;
    public Sprite ui_AvatarImageForNextSelection;

}
