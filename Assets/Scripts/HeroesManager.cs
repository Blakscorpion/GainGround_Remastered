using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HeroesManager : MonoBehaviour
{
    public enum Hero
    {
    ANY,
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
    [SerializeField]
    public List<HeroScriptableObject> availableScriptableObjectsHeroes = new List<HeroScriptableObject>();
    public List<Hero> ListOfDeadHeros = new List<Hero>();
    public List<Hero> ListOfEscapedHeros = new List<Hero>();

    private void Awake() {
        Instance = this;
        // Check if there are already heroes in the PassedHeroes list.
        if (PlayerPrefs.HasKey("SuccessfullHeroNumber0"))
        {
            // load the survivors and remove the PlayerPref data
            for(int i = 0; PlayerPrefs.GetString("SuccessfullHeroNumber"+i).Length > 0; i++) {
                // J'ajoute les héros sauvegardé dans ma liste de héros vivants. Pareil pour la liste des HeroeScriptableObjects
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
        
        foreach (Hero heroAlive in ListOfHeroesAlive)
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
        foreach (Hero hero in ListOfHeroesAlive)
            {
                PlayerPrefs.SetString("SuccessfullHeroNumber"+i, hero.ToString());
                i++;
            }
    }

    public void HeroDead() {
            int index = ListOfHeroesAlive.IndexOf(CurrentHero);
            ListOfHeroesAlive.Remove(CurrentHero);
            ListOfDeadHeros.Add(CurrentHero);
            
            availableScriptableObjectsHeroes.RemoveAt(index);
    }

    public void HeroEscaped() {
            int index = ListOfHeroesAlive.IndexOf(CurrentHero);
            ListOfHeroesAlive.Remove(CurrentHero);
            ListOfEscapedHeros.Add(CurrentHero);

            availableScriptableObjectsHeroes.RemoveAt(index);        
    }

    // Time out - Kill all remaining heroes alive
    public void TimerEnded(){
        // Remove all living heroes that didn't have time to escape, for next level
        foreach (Hero hero in ListOfHeroesAlive){
            ListOfDeadHeros.Add(hero);
        }   

        ListOfHeroesAlive.Clear();    
    }
}
