using UnityEngine;

public class LeaveStage : MonoBehaviour
{
    private GameObject PlayerWhoExits;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player"))
        {
            HeroesManager.Instance.HeroEscaped();
            HeroesManager.Instance.BabyEscaped(collider.gameObject);

            //DialogueManager.Instance.PlayDialogueOnEscape();

            GameManager.Instance.UpdateGameState(GameState.ExitSuccess);

            // Remove from the scene the escaped Hero
            Destroy(collider.gameObject); 
        }
    }
}
