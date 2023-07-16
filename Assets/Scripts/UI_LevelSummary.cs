using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_LevelSummary : MonoBehaviour
{
    private int NextSceneIndex;
    [SerializeField] TextMeshProUGUI EndLevelSummary;
    private void Awake() {
        GameManager.OnGameStateChanged += DisplayGameSummary;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayGameSummary;
    }

    private void DisplayGameSummary(GameState state) {
        if((state == GameState.GameSummary))
        {   
            transform.GetChild(0).gameObject.SetActive(true);
            EndLevelSummary.text = HeroesManager.Instance.ListOfDeadHeros.Count +" hero Dead\n"
                + HeroesManager.Instance.ListOfEscapedHeros.Count +" hero Escaped\n"
                + HeroesManager.Instance.ListOfHeroesAlive.Count + " hero still alive\n";
        }
    }

    public void GoNextLevel(){
        NextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (NextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {   
            SceneManager.LoadScene(NextSceneIndex);
        }
        else{
            Debug.LogError("Error in NextLevel script. It shouldn't be possible to go in this else condition...");
        }
        
    }
}
