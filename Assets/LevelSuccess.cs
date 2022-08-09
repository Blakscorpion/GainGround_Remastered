using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSuccess : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI EndLevelSummary;

    private void Awake() {
        GameManager.OnGameStateChanged += DisplayUIHeroSelection;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayUIHeroSelection;
    }

    private void DisplayUIHeroSelection(GameState state) {
        if((state == GameState.EndStageSummary))
        {   
            Image image = transform.GetComponent<Image>();
            image.enabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
            EndLevelSummary.text = "Level COMPLETED\nSummary\n- "+ HeroesManager.Instance.DeadHeros.Count +" hero Dead";
            GameManager.Instance.UpdateGameState(GameState.NextLevel);
        }
    }
}
