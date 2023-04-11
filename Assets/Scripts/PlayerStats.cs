using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HeroesManager.Hero heroName;
    public int FragNumber = 0;
    public string heroDescription;
    public bool hasABaby=false;   
    public HeroesManager.Hero babyFollowing; 

    private void Awake() {
        GameManager.OnGameStateChanged += AddSuccessBaby;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= AddSuccessBaby;
    }

    private void AddSuccessBaby(GameState state) {
        if(state == GameState.ExitSuccess && hasABaby==true)
        {
            HeroesManager.Instance.PassedHeros.Add(babyFollowing);
            Debug.Log("Baby " + babyFollowing.ToString() +" has passed the exit");
            // Remove the baby from the deadList if there is one (because when you die, you transform as a totem but you enter the deadList)
            HeroesManager.Instance.DeadHeros.Remove(babyFollowing);
        }
    }

    public void SetHeroName(HeroesManager.Hero heroToName)
    {
        heroName = heroToName;
    }

    public void setBabyFollowingName(HeroesManager.Hero baby)
    {
        babyFollowing = baby;
    }
}
