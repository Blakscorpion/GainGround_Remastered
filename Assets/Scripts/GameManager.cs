using System;
using UnityEngine;

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
        UpdateGameState(GameState.PlayerSelection);
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.PlayerSelection:
                ShowPlayerSelectionUI();
                break;
            case GameState.PlayMode:
                break;
            case GameState.Dead:
                DeadHero();
                break;
            case GameState.Pause:
                PauseGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.ExitSuccess:
                ExitSuccess();
                break;
            case GameState.EndStageSummary:
                EndStageSummary();
                break;
            case GameState.NextLevel:
                NextLevel();
                break;
            case GameState.WinGame:
                WinGame();
                break;
        }
        // Si aucun suscriber à cet event, ça renvoi un null, donc on check si y'a bien un suscriber
        OnGameStateChanged?.Invoke(newState);
    }

    private void ShowPlayerSelectionUI() {    
        Debug.Log("PlayerSelection state from GameManager");
        return;
    }
    private void DeadHero() {
        Debug.Log("DeadHero state from GameManager");
        return;
    }
    private void GameOver() { 
        Debug.Log("GameOver state from GameManager"); 
        PauseGame();   
        return;
    }
    private void ExitSuccess() {  
        Debug.Log("ExistSuccess state from GameManager");
        return;
    }
    private void EndStageSummary() {  
        Debug.Log("EndStageSummary state from GameManager");
        return;
    }
    private void NextLevel() {   
        Debug.Log("NextLevel state from GameManager"); 
        //PauseGame();   
        //Go next seen
        //Time.timeScale = 1;
        return;
    }
    private void WinGame() {   
        Debug.Log("WinGame state from GameManager"); 
        //PauseGame();   
        //Go next seen
        //Time.timeScale = 1;
        return;
    }

    void PauseGame ()
    {
        // Clean the PlayerPref to avoid starting with the heroes of the previous session
        PlayerPrefs.DeleteAll();
    }

    
}

public enum GameState {

    PlayerSelection,
    PlayMode,
    Dead,
    Pause,
    GameOver,
    ExitSuccess,
    EndStageSummary,
    NextLevel,
    WinGame
}
