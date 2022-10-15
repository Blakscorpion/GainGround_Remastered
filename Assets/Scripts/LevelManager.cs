using UnityEngine;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;    
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public int EnnemyNumber;
    
    [SerializeField] TextMeshProUGUI UIEnnemyRemaining;
    [SerializeField] TextMeshProUGUI UITimeRemaining;


    void Awake()
    {
        Instance=this;
    }
    
    void Start()
    {
        EnnemyNumber = GameObject.FindGameObjectsWithTag("Ennemy").Length;
        UIEnnemyRemaining.text= EnnemyNumber.ToString();        
        timerIsRunning = true;
    }

    private void Update() {
        // Level Timer management
        if (timerIsRunning)
        {
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }
            
            else{
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
            // We update the UI with the timer each frame
            UITimeRemaining.text = Mathf.FloorToInt(timeRemaining).ToString();
        }
    }

    public void RemoveEnnemy() {
        EnnemyNumber--;
        UIEnnemyRemaining.text=EnnemyNumber.ToString();
        if (EnnemyNumber<=0)
        {
            GameManager.Instance.UpdateGameState(GameState.EndStageSummary);
        }
    }
}
