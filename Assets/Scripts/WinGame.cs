using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
[SerializeField] TextMeshProUGUI WinGameUI;

    private void Awake() {
        GameManager.OnGameStateChanged += DisplayUIHeroSelection;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= DisplayUIHeroSelection;
    }
    private void DisplayUIHeroSelection(GameState state) {
        if((state == GameState.WinGame))
        {   
            Image image = transform.GetComponent<Image>();
            image.enabled=true;
            transform.GetChild(0).gameObject.SetActive(true);
            WinGameUI.text = "You WON !\nGame FINISHED !"+ HeroesManager.Instance.ListOfEscapedHeros.Count + HeroesManager.Instance.ListOfHeroesAlive.Count +" hero passed the gate !";
            Time.timeScale = 0;
        }
    }
}
