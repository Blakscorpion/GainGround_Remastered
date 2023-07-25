using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_GameOver : MonoBehaviour
{
    private void Awake() {
        GameManager.OnGameStateChanged += DisplayGameOverUI;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayGameOverUI;
    }

    
    private void DisplayGameOverUI(GameState state) {
        if(state == GameState.GameOver)
        {   
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void BackToMainMenu(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
