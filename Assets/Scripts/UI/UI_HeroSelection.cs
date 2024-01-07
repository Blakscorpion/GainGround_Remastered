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
    private List<HeroScriptableObject> availableScriptableObjectsHeroes = new List<HeroScriptableObject>();
    [SerializeField] private GameObject PlayerToInstantiate;
    [SerializeField] private Transform InitialPlayerLocation;
    [SerializeField] private TextMeshProUGUI HeroSelectionUI;
    [SerializeField] private Sprite heroSelectionPortrait;
    [SerializeField] private Sprite weapon1Icon;
    [SerializeField] private TextMeshProUGUI weapon1Description;
    [SerializeField] private Sprite weapon2Icon;
    [SerializeField] private TextMeshProUGUI weapon2Description;
    [SerializeField] private Sprite selectedHeroFrame;
    [SerializeField] private Sprite selectedHeroFramePrevious;
    [SerializeField] private Sprite selectedHeroFrameNext;
    
    [Header("UI To enable/Disable")]
    [SerializeField] private GameObject Timer;
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject HeroSelectedPanel;
    [SerializeField] private GameObject LevelInfo;

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

        //Load in memory all Heroes ScriptableObject (to have all avatars/protraits...)
        


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
                HeroesManager.Instance.HeroSelected();
                
                Timer.SetActive(false);
                Background.SetActive(false);
                HeroSelectedPanel.SetActive(false);
                LevelInfo.SetActive(false);
            }
        }
    }

    private void DisplayUIHeroSelection(GameState state){
        if(state == GameState.PlayerSelection){   
            //Retrieve the number of heros alive
            availableHeroList = HeroesManager.Instance.ListOfAvailableHeroes;
            availableScriptableObjectsHeroes = HeroesManager.Instance.availableScriptableObjectsHeroes;
            lengthHeroList = availableHeroList.Count;
            currentHeroIndexselected=0;

            // We check if the list is not empty just in case, but this process should already have been made by the gameManager
            if (lengthHeroList>0){
                UpdateHeroSelection();
                isUIActive=true;
                Timer.SetActive(true);
                Background.SetActive(true);
                HeroSelectedPanel.SetActive(true);
                LevelInfo.SetActive(true);
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
        UpdateHeroSelection();
    }

    public void displayNextHero()
    {
        currentHeroIndexselected+=1;
        UpdateHeroSelection();
    }

    private void UpdateHeroSelection()
    {
        
        int tmpPreviousIndex=currentHeroIndexselected-1;
        if (tmpPreviousIndex<0){
            tmpPreviousIndex = lengthHeroList+tmpPreviousIndex;
        } 
        int selectedIndex = currentHeroIndexselected%lengthHeroList;                
        HeroSelectionUI.text = "" + availableScriptableObjectsHeroes[selectedIndex].HeroName;
        heroSelectionPortrait = availableScriptableObjectsHeroes[selectedIndex].UIPortraitForSelectionScreen;
        weapon1Icon = availableScriptableObjectsHeroes[selectedIndex].UIWeaponIcon1;
        weapon1Description.text = "" + availableScriptableObjectsHeroes[selectedIndex].primaryWeaponDescription;
        weapon2Icon = availableScriptableObjectsHeroes[selectedIndex].UIWeaponIcon1;
        weapon2Description.text = "" + availableScriptableObjectsHeroes[selectedIndex].secondaryWeaponDescription;
        selectedHeroFrame = availableScriptableObjectsHeroes[selectedIndex].UIPortraitForNextSelection;
        selectedHeroFramePrevious = availableScriptableObjectsHeroes[tmpPreviousIndex%lengthHeroList].UIPortraitForNextSelection;
        selectedHeroFrameNext = availableScriptableObjectsHeroes[(currentHeroIndexselected+1)%lengthHeroList].UIPortraitForNextSelection;
    }
}
