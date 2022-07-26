using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool readyToGoToNextLevel = false; 
    private int NextSceneIndex;
    private void Awake() {
        GameManager.OnGameStateChanged += GoToNextLevel;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= GoToNextLevel;
        readyToGoToNextLevel = false;
    }

    private void GoToNextLevel(GameState state) {
        if((state == GameState.NextLevel))
        {   
            readyToGoToNextLevel=true;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (readyToGoToNextLevel)
            {
                Debug.Log("Reprise du trafic"); 
                Time.timeScale = 1;
                NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (NextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(NextSceneIndex);
                }
                else{
                    GameManager.Instance.UpdateGameState(GameState.WinGame);
                }
            }
        }
    }
}
