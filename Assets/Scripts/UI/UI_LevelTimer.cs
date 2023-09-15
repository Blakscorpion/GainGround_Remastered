using UnityEngine;
using TMPro;

public class UI_LevelTimer : MonoBehaviour
{
    private float timeRemaining;
    private bool timerIsRunning = false;
    [SerializeField] TextMeshProUGUI UI_TimeRemaining;
    
    void Start()
    {    
        // we take the timer value from the Level Manager. If it's empty or wrong, we put arbitrarily 115s
        timeRemaining = LevelManager.Instance.TimeForThisLevel;
        if (timeRemaining <=0){
            timeRemaining = 115;
        }

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
                timerIsRunning = false;
                timeRemaining = 0;
                GameManager.Instance.UpdateGameState(GameState.StageEnd); 
            }
            
            // We update the UI with the timer each frame
            UI_TimeRemaining.text = Mathf.FloorToInt(timeRemaining).ToString();
        }
    }
}
