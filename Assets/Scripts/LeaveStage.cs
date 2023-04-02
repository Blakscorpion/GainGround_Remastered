using UnityEngine;

public class LeaveStage : MonoBehaviour
{
    private GameObject PlayerWhoExits;

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player"))
        {
            // Deactivate the movement, collider, animation of the player that exits
            PlayerWhoExits = collider.gameObject;
            PlayerWhoExits.GetComponent<Collider2D>().enabled=false;
            PlayerWhoExits.GetComponent<Animator>().enabled = false;
            PlayerWhoExits.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log(collider.name + " PASSED !");

            // Change state to Exist Success
            GameManager.Instance.UpdateGameState(GameState.ExitSuccess);
            
            // Destroy the player
            Destroy(PlayerWhoExits, 0.5f); 
            
            // TODO Play song of plyaer success 
        }
    }
}
