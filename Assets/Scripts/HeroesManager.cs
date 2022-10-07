using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HeroesManager : MonoBehaviour
{
    public enum Hero
    {
    Athra,
    Betty,
    Cyber,
    FireKnight,
    Gascon,
    General,
    GlowKnight,
    Honey,
    Johnny,
    Kid,
    Kou,
    Mam,
    Mars,
    MudPuppy,
    Professor,
    Robby,
    Valkyrie,
    Verbal,
    WaterKnight,
    Zaemon
    }

    public static HeroesManager Instance;
    public Hero CurrentHero;

    public List<Hero> ListOfHeroesAlive = new List<Hero>();
    public List<Hero> DeadHeros = new List<Hero>();
    public List<Hero> PassedHeros = new List<Hero>();

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += RemoveHeroFromList;
        GameManager.OnGameStateChanged += AddSuccessHero;

        // Check if there are already heroes in the PassedHeroes list.
        if (PlayerPrefs.HasKey("SuccessfullHeroNumber0"))
        {
            // load the survivors and remove the PlayerPref data
            for(int i = 0; PlayerPrefs.GetString("SuccessfullHeroNumber"+i).Length > 0; i++) {
                // Là c'est un peu chiadé mais en gros je "cast" mon string en enum de type Hero pour l'ajouter dans ma liste.
                ListOfHeroesAlive.Add((Hero)Enum.Parse(typeof(Hero),PlayerPrefs.GetString("SuccessfullHeroNumber"+i)));
                PlayerPrefs.DeleteKey("SuccessfullHeroNumber"+i);
            }     
        }
        else {
            // Otherwise we consider that it's level one and we load the 3 default heroes
            ListOfHeroesAlive.Add(Hero.Athra);
            ListOfHeroesAlive.Add(Hero.Betty);
            ListOfHeroesAlive.Add(Hero.Cyber);
        }

    }
    private void OnDisable() { 
        //When the scene is closing (onDisable) : Register the Heroes that succeded in the PlayerPref, to load them in next scene
        int i = 0;
        foreach (Hero hero in PassedHeros)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero.ToString());
                i++;
            }
        foreach (Hero hero in ListOfHeroesAlive)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero.ToString());
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
        List<Hero> tmp_ListOfHeroesAlive = new List<Hero>();
        
        foreach (Hero hero in ListOfHeroesAlive)
        {
            tmp_ListOfHeroesAlive.Add(hero);
        }   
        
        foreach (Hero hero in tmp_ListOfHeroesAlive)
        {
            ListOfHeroesAlive.Remove(hero);
            DeadHeros.Add(hero);
        }        
    }
}
