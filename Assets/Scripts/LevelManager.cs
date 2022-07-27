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
                if (HeroesManager.Instance.PassedHeros.Count>0)
                {
                    EndLevelSummary.transform.parent.gameObject.SetActive(true);
                    EndLevelSummary.text = "TIME OUT !\nLevel COMPLETED\nSummary\n- "+ HeroesManager.Instance.PassedHeros.Count +" hero passed the gate !";
                    GameManager.Instance.UpdateGameState(GameState.NextLevel);
                }
                else{
                    EndLevelSummary.transform.parent.gameObject.SetActive(true);
                    EndLevelSummary.text = "TIME OUT !\nSummary\n- NO hero passed the gate !\nGAMES OVER !! (noob)";
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
            EndLevelSummary.transform.parent.gameObject.SetActive(true);

            EndLevelSummary.text = "Level COMPLETED\nSummary\n- All ennemies Killed\n- "+ HeroesManager.Instance.DeadHeros.Count +" hero Dead";
            GameManager.Instance.UpdateGameState(GameState.NextLevel);
        }
    }
}
