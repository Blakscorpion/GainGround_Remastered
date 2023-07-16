using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

class UI_HeroSelection : MonoBehaviour
{
    private int currentHeroIndexselected=0;
    private int lengthHeroList=0;
    private List<HeroesManager.Hero> availableHeroList = new List<HeroesManager.Hero>();
    [SerializeField] private TextMeshProUGUI HeroSelectionUI;
    [SerializeField] private GameObject PlayerToInstantiate;
    [SerializeField] private Transform InitialPlayerLocation;
    private bool isUIActive = false;


    private void Awake() {
        GameManager.OnGameStateChanged += DisplayUIHeroSelection;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayUIHeroSelection;
    }

    private void OnEnable() {
        currentHeroIndexselected=0;
    }

    void Start()
    {   
        //Find the HeroSpawnLocation transform if not assigned yet
        if (InitialPlayerLocation == null){
            GameObject location = GameObject.Find("HeroSpawnLocation");
            if (location == null){
                Debug.LogError("Please assign the HeroSpawnLocation of the Map, to the UI_HeroSelection script available in the HeroSelectPanel");
            }
            else {
                InitialPlayerLocation = location.transform;
            } 
        }
    }

    void Update()
    {
        if (isUIActive)
            {
            
            if (Input.GetKeyDown(KeyCode.RightArrow)){
                displayNextHero();     
            }

            else if(Input.GetKeyDown(KeyCode.LeftArrow)){
                displayPreviousHero();     
            }

            else if (Input.GetKeyDown(KeyCode.Return)){
                //currentHeroIndexselected à instancier/activer
                HeroesManager.Instance.CurrentHero=availableHeroList[currentHeroIndexselected%lengthHeroList];
                
                transform.GetChild(0).gameObject.SetActive(false);
                isUIActive=false;

                //Instantiate the Hero selected (Hero prefab set through the editor field : "PlayerToInstantiate)
                GameObject HeroGenerated = Instantiate(PlayerToInstantiate, InitialPlayerLocation.position, InitialPlayerLocation.rotation);
                HeroGenerated.SendMessage("InitHero", HeroesManager.Instance.CurrentHero);
                GameManager.Instance.UpdateGameState(GameState.PlayMode);
            }
        }
    }

    private void DisplayUIHeroSelection(GameState state){
        if(state == GameState.PlayerSelection){   
              //Retrieve the number of heros alive
            availableHeroList = HeroesManager.Instance.ListOfHeroesAlive;
            lengthHeroList = availableHeroList.Count;
            currentHeroIndexselected=0;

            // We check if the list is not empty just in case, but this process should already have been made by the gameManager
            if (lengthHeroList>0){
                transform.GetChild(0).gameObject.SetActive(true);
                UpdateTextHeroSelection();
                isUIActive=true;
            }
            else{
                Debug.LogError("There are no available Hero. The gameManager shouldn't have triggerred the PlayerSelection state !");
            }
        } 
    }

    public void displayPreviousHero()
    {
        currentHeroIndexselected-=1;
        if (currentHeroIndexselected<0){
            currentHeroIndexselected = lengthHeroList-1;
        }     
        UpdateTextHeroSelection();
    }

    public void displayNextHero()
    {
        currentHeroIndexselected+=1;
        UpdateTextHeroSelection();
    }

    private void UpdateTextHeroSelection()
    {
        HeroSelectionUI.text = "Select Hero: " + availableHeroList[currentHeroIndexselected%lengthHeroList];
    }
}
