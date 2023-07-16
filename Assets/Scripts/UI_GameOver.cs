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
        }
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
