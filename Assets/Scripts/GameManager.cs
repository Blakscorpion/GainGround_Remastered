using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start() {
        DialogueManager.Instance.PlayDialogueOnStartingLevel();
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.PlayerSelection:
                PlayerSelection();
                break;
            case GameState.PlayMode:
                PlayMode();
                break;
            case GameState.Dialogue:
                Dialogue();
                break;
            case GameState.Dead:
                CheckRemainingHeroes();
                break;
            case GameState.Pause:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.ExitSuccess:
                ExitSuccess();
                CheckRemainingHeroes();
                break;
            case GameState.StageEnd:
                StageEndCheck();
                break;
            case GameState.NextLevel:
                NextLevel();
                break;
            case GameState.GameSummary:
                GameSummary();
                break;
            case GameState.WinGame:
                WinGame();
                break;
        }
        // Si aucun suscriber à cet event, ça renvoi un null, donc on check si y'a bien un suscriber
        OnGameStateChanged?.Invoke(newState);
    }

    private void PlayerSelection() {    
        Debug.Log("== GAMESTATE : 'PLAYERSELECTION' ==");
        return;
    }

    private void Dialogue() {    
        Debug.Log("== GAMESTATE : 'DIALOGUE' ==");
        return;
    }

    // We check if there are remaining heroes alive. If yes we go to the selection screen, if not we ends the stage.
    private void CheckRemainingHeroes() {
        Debug.Log("======= GAMESTATE : 'DEAD or EXIT' =======");
        Debug.Log("We check if there are remaining heroes in the list");
        if (HeroesManager.Instance.ListOfAvailableHeroes == null || HeroesManager.Instance.ListOfAvailableHeroes.Count == 0){
            UpdateGameState(GameState.StageEnd);
            Debug.Log("No more heroes --> StageEnd");
        }
        else{
            UpdateGameState(GameState.PlayerSelection);
            Debug.Log("Heroes Remaining --> PlayerSelection");
        }
        return;
    }

    private void GameOver() { 
        Debug.Log("===== GAMESTATE : 'GAMEOVER' =====");
        return;
    }

    private void ExitSuccess() {  
        Debug.Log("=== GAMESTATE : 'EXISTSUCCESS' ===");
        Debug.Log("We check if there are remaining heroes in the list");
        return;
    }

    private void StageEndCheck() {  
        Debug.Log("===== GAMESTATE : 'STAGE_END' ====");
        Debug.Log("We check if it's GameOver or if we go to the next level");
        
        // All ennemies have been killed --> NextLevel
        if (LevelManager.Instance.currentEnnemyNumber <= 0){
            UpdateGameState(GameState.NextLevel);
            Debug.Log("No more ennemies --> NextLevel (by cleaning)");
        }
        //No more heroes available
        else if (HeroesManager.Instance.ListOfAvailableHeroes.Count == 0){
            Debug.Log("No more Heroes available");
            if (HeroesManager.Instance.ListOfEscapedHeros.Count > 0){
                Debug.Log("But some of them escaped --> NextLevel (by escaping)" );
                UpdateGameState(GameState.NextLevel);
            }
            else{
                Debug.Log("But nobody escaped --> GAMEOVER" );
                UpdateGameState(GameState.GameOver);
            }
        }
        // Timer Ended
        else{
            Debug.Log("Timer Ended" );
            if (HeroesManager.Instance.ListOfEscapedHeros.Count > 0){
                Debug.Log("But some Heroes escaped --> NextLevel (by escaping)" );
                HeroesManager.Instance.TimerEnded();
                UpdateGameState(GameState.NextLevel);
            }
            else{
                Debug.Log("But nobody escaped --> GAMEOVER" );
                UpdateGameState(GameState.GameOver);
            }
        }
        return;
    }

    private void GameSummary() {   
        Debug.Log("=== GAMESTATE : 'GAMESUMMARY' ===");
        return;
    }

    private void NextLevel() {   
        Debug.Log("=== GAMESTATE : 'NEXT_LEVEL' ===");
        
        //We check if it was the last level
        int NextSceneIndex;
        NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //If it's not, we display the game summary before the next level
        if (NextSceneIndex < SceneManager.sceneCountInBuildSettings){
            GameManager.Instance.UpdateGameState(GameState.GameSummary);
        }
        //If it was the last Level, we display the Win game UI
        else{    
            GameManager.Instance.UpdateGameState(GameState.WinGame);
        }
        return;
    }

    private void WinGame() {   
        Debug.Log("=== GAMESTATE : 'WIN_GAME' ==="); 
        //PauseGame();   
        //Go next seen
        //Time.timeScale = 1;
        return;
    }

    void PlayMode()
    {
        Debug.Log("===== GAMESTATE : 'PLAYMODE' =====");
    }

    void PauseGame()
    {
        Debug.Log("===== GAMESTATE : 'PAUSE' =====");
        Time.timeScale = 0;
    }    
}

public enum GameState {

    PlayerSelection,
    PlayMode,
    Dialogue,
    Dead,
    Pause,
    GameOver,
    ExitSuccess,
    StageEnd,
    NextLevel,
    GameSummary,
    WinGame
}
