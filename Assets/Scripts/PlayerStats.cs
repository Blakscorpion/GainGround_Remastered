using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HeroesManager.Hero heroName;
    public int FragNumber = 0;
    public string heroDescription;
    public bool hasABaby=false;   
    public HeroesManager.Hero babyFollowing; 


    public void SetHeroName(HeroesManager.Hero heroToName)
    {
        heroName = heroToName;
    }

    public void setBabyFollowingName(HeroesManager.Hero baby)
    {
        babyFollowing = baby;
    }
}
