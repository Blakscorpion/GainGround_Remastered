using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
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
            Image image = transform.GetComponent<Image>();
            image.enabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
