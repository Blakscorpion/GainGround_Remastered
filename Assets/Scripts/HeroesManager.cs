using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HeroesManager : MonoBehaviour
{
    public static HeroesManager Instance;
    public String CurrentHero = "Player";

    public List<string> ListOfHeroesAlive = new List<string>();
    public List<string> DeadHeros = new List<string>();
    public List<string> PassedHeros = new List<string>();

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += RemoveHeroFromList;
        GameManager.OnGameStateChanged += AddSuccessHero;

        // Check if there are already heroes in the PassedHeroes list.
        if (PlayerPrefs.HasKey("SuccessfullHeroNumber0"))
        {
            // load the survivors and remove the PlayerPref data
            for(int i = 0; PlayerPrefs.GetString("SuccessfullHeroNumber"+i).Length > 0; i++) {
                ListOfHeroesAlive.Add(PlayerPrefs.GetString("SuccessfullHeroNumber"+i));
                PlayerPrefs.DeleteKey("SuccessfullHeroNumber"+i);
            }     
        }
        else {
            // Otherwise it's level one and we load the default heroes
            ListOfHeroesAlive.Add("Player");
            ListOfHeroesAlive.Add("Poilux2");
            ListOfHeroesAlive.Add("Poilux3");
        }

    }
    private void OnDisable() { 
        //When the scene is closing (onDisable) : Register the Heroes that succeded in the PlayerPref, to load them in next scene
        int i = 0;
        foreach (string hero in PassedHeros)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero);
                i++;
            }
        foreach (string hero in ListOfHeroesAlive)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero);
                i++;
            }
    }
    
    private void OnDestroy() {
        GameManager.OnGameStateChanged -= RemoveHeroFromList;
        GameManager.OnGameStateChanged -= AddSuccessHero;
    }
    
    void Start()
    {
        
        
    }

    private void RemoveHeroFromList(GameState state) {
        if(state == GameState.Dead)
        {
            if (ListOfHeroesAlive.Count > 0)
            {
                ListOfHeroesAlive.Remove(CurrentHero);
                DeadHeros.Add(CurrentHero);
            }
        }
    }

    private void AddSuccessHero(GameState state) {
        if(state == GameState.ExitSuccess)
        {
            Debug.Log("Exit Success From Listener AddSuccessHero");
            ListOfHeroesAlive.Remove(CurrentHero);
            PassedHeros.Add(CurrentHero);
            if (ListOfHeroesAlive.Count == 0)
            {
                Debug.Log("Plus de héro donc state : EndStage");
                GameManager.Instance.UpdateGameState(GameState.EndStageSummary);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.PlayerSelection);
            }
        }
    }

    // Time out - Kill all remaining heroes alive
    public void TimerEnded()
    {
        // Need to create a temporary copy of my list, because I can't delete on the fly inside my list within a foreach
        List<string> tmp_ListOfHeroesAlive = new List<string>();
        
        foreach (var hero in ListOfHeroesAlive)
        {
            tmp_ListOfHeroesAlive.Add(hero);
        }   
        
        foreach (var hero in tmp_ListOfHeroesAlive)
        {
            ListOfHeroesAlive.Remove(hero);
            DeadHeros.Add(hero);
        }        
    }
}
