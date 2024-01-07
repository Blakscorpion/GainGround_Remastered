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
    Zaemon,
    CURRENT,
    ANY1,
    ANY2,
    ANY3,
    ANY4,
    NONE    
    }

    public static HeroesManager Instance;
    public Hero CurrentHero;

    public List<Hero> ListOfAvailableHeroes = new List<Hero>();
    public List<Hero> ListOfEscapedHeros = new List<Hero>();
    public List<Hero> ListOfDeadHeros = new List<Hero>();
    [SerializeField]
    public List<HeroScriptableObject> availableScriptableObjectsHeroes = new List<HeroScriptableObject>();

    private void Awake() {
        Instance = this;
        ListOfAvailableHeroes.Clear();
        ListOfEscapedHeros.Clear();
        ListOfDeadHeros.Clear();

        // Check if there are already heroes in the PassedHeroes list.
        if (PlayerPrefs.HasKey("SuccessfullHeroNumber0"))
        {
            // load the survivors and remove the PlayerPref data
            for(int i = 0; PlayerPrefs.GetString("SuccessfullHeroNumber"+i).Length > 0; i++) {
                // J'ajoute les héros sauvegardé dans ma liste de héros vivants. Pareil pour la liste des HeroeScriptableObjects
                ListOfAvailableHeroes.Add((Hero)Enum.Parse(typeof(Hero),PlayerPrefs.GetString("SuccessfullHeroNumber"+i)));
                
                PlayerPrefs.DeleteKey("SuccessfullHeroNumber"+i);
            }     
            
        }
        else {
            // Otherwise we consider that it's level one and we load the 3 default heroes
            ListOfAvailableHeroes.Add(Hero.Athra);
            ListOfAvailableHeroes.Add(Hero.Betty);
            ListOfAvailableHeroes.Add(Hero.Cyber);
        }
        
        foreach (Hero heroAlive in ListOfAvailableHeroes)
        {
            availableScriptableObjectsHeroes.Add(Resources.Load<HeroScriptableObject>("Heroes/" + heroAlive.ToString()));
        }

    }
    private void OnDisable() { 
        //When the scene is closing (onDisable) : Register the Heroes that succeded in the PlayerPref, to load them in next scene
        int i = 0;
        foreach (Hero hero in ListOfEscapedHeros)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero.ToString());
                i++;
            }
        foreach (Hero hero in ListOfAvailableHeroes)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero.ToString());
                i++;
            }
    }

    public void HeroSelected() {
        int index = ListOfAvailableHeroes.IndexOf(CurrentHero);
        ListOfAvailableHeroes.Remove(CurrentHero);            
        availableScriptableObjectsHeroes.RemoveAt(index);
    }

    public void HeroEscaped() {
        ListOfEscapedHeros.Add(CurrentHero);      
    }

    public void HeroDead() {
        ListOfDeadHeros.Add(CurrentHero);
    }

    //Add baby to escaped heroes, if there is one and remove it from the dead list
    public void BabyEscaped(GameObject heroPassed){
            PlayerStats _infoPlayer = heroPassed.GetComponent<PlayerStats>();
            if (_infoPlayer.hasABaby==true){        
                ListOfEscapedHeros.Add(_infoPlayer.babyFollowing);
                Debug.Log("Baby " + _infoPlayer.babyFollowing.ToString() +" has passed the exit");
                // Remove the baby from the deadList if there is one (because when you die, you transform as a totem but you enter the deadList)
                ListOfDeadHeros.Remove(_infoPlayer.babyFollowing);
            }
    }
    
    

    // Time out - Kill all remaining heroes alive
    public void TimerEnded(){
        // Remove all living heroes that didn't have time to escape, for next level
        foreach (Hero hero in ListOfAvailableHeroes){
            ListOfDeadHeros.Add(hero);
        }   

        ListOfAvailableHeroes.Clear();    
    }
}
