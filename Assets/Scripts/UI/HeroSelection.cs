using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HeroSelection : MonoBehaviour
{
    private int currentHeroIndexselected=0;
    public int lengthHeroList=0;
    public HeroesManager.Hero CurrentPlayer;
    private List<HeroesManager.Hero> availableHeroList = new List<HeroesManager.Hero>();
    [SerializeField] TextMeshProUGUI HeroSelectionUI;
    public GameObject PlayerToInstantiate;
    public Transform InitialPlayerLocation;


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

                //Instantiate the Player selected
                Debug.Log("I want to activate : " + availableHeroList[currentHeroIndexselected%lengthHeroList]);
                GameObject HeroGenerated = Instantiate(PlayerToInstantiate, InitialPlayerLocation.position, InitialPlayerLocation.rotation);
                HeroGenerated.SendMessage("InitHero", HeroesManager.Instance.CurrentHero);
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
        currentHeroIndexselected=0;

        // If some heros are still alive, we display the player selection list to continue the game
        if (lengthHeroList>0){
            Image image = transform.GetComponent<Image>();
            image.enabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
            HeroSelectionUI.text = "Select Your Hero : " + availableHeroList[0];
            CurrentPlayer = availableHeroList[currentHeroIndexselected%lengthHeroList];
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
}
