using System;
using System.Collections;
using System.Collections.Generic;
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
        UpdateGameState(GameState.PlayMode);
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
        }
        // Si aucun suscriber à cet event, ça renvoi un null, donc on check si y'a bien un suscriber
        OnGameStateChanged?.Invoke(newState);
    }

    private void ShowPlayerSelectionUI() {    
        return;
    }
    private void DeadHero() {

    }
    private void GameOver() { 
        PauseGame();   
        return;
    }
    private void ExitSuccess() {  
        Debug.Log("ExistSuccess from GameManager");  
        return;
    }
    private void EndStageSummary() {  
        return;
    }
    private void NextLevel() {   
        Debug.Log("NEXT LEVEL !"); 
        //PauseGame();   
        //Go next seen
        //Time.timeScale = 1;
        return;
    }

    void PauseGame ()
    {
        Time.timeScale = 0;
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
    NextLevel
}
