using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    private void Awake() {
        GameManager.OnGameStateChanged += DisplaySelectionPlayerUI;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplaySelectionPlayerUI;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void DisplaySelectionPlayerUI(GameState state) {
        // PanelSelectionPlayer.SetActive(state == GameState.PlayerSelection);
    }
}
