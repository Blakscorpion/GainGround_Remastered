using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WinGame : MonoBehaviour
{
    private void Awake() {
        GameManager.OnGameStateChanged += DisplayUIWinGame;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayUIWinGame;
    }

    private void DisplayUIWinGame(GameState state) {
        if((state == GameState.WinGame))
        {   
            transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
