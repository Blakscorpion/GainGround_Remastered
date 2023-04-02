using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero", menuName = "Hero")]
public class Hero : ScriptableObject
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

}
