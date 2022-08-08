using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{
    private int currentHeroIndexselected=0;
    public int lengthHeroList=0;
    public GameObject CurrentPlayer;
    private List<string> availableHeroList = new List<string>();
    [SerializeField] TextMeshProUGUI HeroSelectionUI;


    private void Awake() {
        GameManager.OnGameStateChanged += DisplayUIHeroSelection;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayUIHeroSelection;
    }
    private void OnEnable() {
        currentHeroIndexselected=0;
    }


    void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
            {
            
            // Récupérer le prochain héros dispo et l'afficher
            if ((Input.GetKeyDown(KeyCode.RightArrow)) && (lengthHeroList > 0))
            {
                currentHeroIndexselected+=1;
                
                Debug.Log("Nombre de héros vivants : " + lengthHeroList);
                Debug.Log(currentHeroIndexselected%lengthHeroList);
                HeroSelectionUI.text = "Select Your Hero : " + availableHeroList[currentHeroIndexselected%lengthHeroList];     
            }
            // Récupérer le précédent héros dispo et l'afficher
            else if((Input.GetKeyDown(KeyCode.LeftArrow)) && (lengthHeroList > 0))
            {
                currentHeroIndexselected-=1;
                if (currentHeroIndexselected<0)
                {
                    currentHeroIndexselected = lengthHeroList-1;
                }       
                Debug.Log("Nombre de héros vivants : " + lengthHeroList);
                Debug.Log(currentHeroIndexselected%lengthHeroList);
                HeroSelectionUI.text = "Select Your Hero : " + availableHeroList[currentHeroIndexselected%lengthHeroList];     

            }

            else if ((Input.GetKeyDown(KeyCode.Return)) && (lengthHeroList > 0))
            {
                
                //currentHeroIndexselected à instancier/activer
                HeroesManager.Instance.CurrentHero=availableHeroList[currentHeroIndexselected%lengthHeroList];
                Debug.Log("Selection validée : " + availableHeroList[currentHeroIndexselected%lengthHeroList]);
                
                // Hide the UI
                Image image = transform.GetComponent<Image>();
                image.enabled=false;
                transform.GetChild(0).gameObject.SetActive(false);

                //Activate the Player selected
                Debug.Log("I want to activate : " + availableHeroList[currentHeroIndexselected%lengthHeroList]);
                CurrentPlayer = FindInActiveObjectByName(availableHeroList[currentHeroIndexselected%lengthHeroList]);
                CurrentPlayer.SetActive(true);
                GameManager.Instance.UpdateGameState(GameState.PlayMode);

            }
        }
    }

    private void DisplayUIHeroSelection(GameState state) {
        if((state == GameState.Dead) || (state == GameState.PlayerSelection))
        {   
            StartCoroutine(WaitABit());
            //display remaining heroes selection, or gameover if no more available.
            DisplayHeroSelectionUIorGAMEOVER();
        }
        
    }
    IEnumerator WaitABit()
    {
        yield return new WaitForSecondsRealtime(0.7f);
    }

    private void DisplayHeroSelectionUIorGAMEOVER()
    {
        //Retrieve the number of heros alive
        availableHeroList = HeroesManager.Instance.ListOfHeroesAlive;
        lengthHeroList = availableHeroList.Count;

        // If some heros are still alive, we display the player selection list to continue the game
        if (lengthHeroList>0){
            Image image = transform.GetComponent<Image>();
            image.enabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
            HeroSelectionUI.text = "Select Your Hero : " + availableHeroList[0];
            CurrentPlayer = FindInActiveObjectByName(availableHeroList[currentHeroIndexselected%lengthHeroList]);
        }

        // IF no more heroes available, either it's game over, either we go to level summary if some of them went through exit gate
        else{
            if (HeroesManager.Instance.PassedHeros.Count == 0)
            {
                GameManager.Instance.UpdateGameState(GameState.GameOver);     
            }
            else{
                GameManager.Instance.UpdateGameState(GameState.EndStageSummary);
            }
        }
    }

    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
