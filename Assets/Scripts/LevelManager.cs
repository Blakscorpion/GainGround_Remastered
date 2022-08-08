using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public int EnnemyNumber;
    
    [SerializeField] TextMeshProUGUI UIEnnemiyRemaining;
    [SerializeField] TextMeshProUGUI UITimeRemaining;
    [SerializeField] TextMeshProUGUI GameOverPanel;
    [SerializeField] TextMeshProUGUI EndLevelSummary;
    public TextMeshProUGUI WinPannel;


    void Awake()
    {
        Instance=this;
    }
    
    void Start()
    {
        EnnemyNumber = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        UIEnnemiyRemaining.text="Ennemies : "+EnnemyNumber.ToString();        
        timerIsRunning = true;
    }

    private void Update() {
        // Level Timer management
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UITimeRemaining.text = "Time : " + Mathf.FloorToInt(timeRemaining).ToString();
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                //We kill all remaining heros alive
                HeroesManager.Instance.TimerEnded();

                // If some heroes went through exit, we go to next level, otherwise it's game over.
                if (HeroesManager.Instance.PassedHeros.Count>0)
                {
                    GameManager.Instance.UpdateGameState(GameState.EndStageSummary);
                }
                else{
                    GameManager.Instance.UpdateGameState(GameState.GameOver);
                }
                
            }
        }
    }

    public void RemoveEnnemy() {
        EnnemyNumber--;
        UIEnnemiyRemaining.text=EnnemyNumber.ToString() + " Ennemies";
        if (EnnemyNumber<=0)
        {
            GameManager.Instance.UpdateGameState(GameState.EndStageSummary);
        }
    }
}
