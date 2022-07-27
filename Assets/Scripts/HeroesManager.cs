using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class HeroesManager : MonoBehaviour
{
    public static HeroesManager Instance;
    public String CurrentHero = "Player";
    [SerializeField] TextMeshProUGUI EndLevelSummary;

    public List<string> ListOfHeroesAlive = new List<string>();
    public List<string> DeadHeros = new List<string>();
    public List<string> PassedHeros = new List<string>();

    private void Awake() {
        Instance = this;
        GameManager.OnGameStateChanged += RemoveHeroFromList;
        GameManager.OnGameStateChanged += AddSuccessHero;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= RemoveHeroFromList;
        GameManager.OnGameStateChanged -= AddSuccessHero;
    }
    
    // Start is called before the first frame update TESTForGIT
    void Start()
    {
        ListOfHeroesAlive.Add("Player");
        ListOfHeroesAlive.Add("Poilux2");
        ListOfHeroesAlive.Add("Poilux3");
    }


    private void RemoveHeroFromList(GameState state) {
        if(state == GameState.Dead)
        {
            ListOfHeroesAlive.Remove(CurrentHero);
            DeadHeros.Add(CurrentHero);
            if (ListOfHeroesAlive.Count == 0)
            {
                CurrentHero="No more Heroes";
                Debug.Log("Current Hero After death : " + CurrentHero);
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
                EndLevelSummary.transform.parent.gameObject.SetActive(true);
                EndLevelSummary.text = "Level COMPLETED\nSummary\n- "+ HeroesManager.Instance.DeadHeros.Count +" hero Dead";
                GameManager.Instance.UpdateGameState(GameState.NextLevel);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.PlayerSelection);
            }
        }
    }
}
