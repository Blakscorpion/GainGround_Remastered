using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {

        //If player touches the ennemy
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            //Freeze player, then delete after 1sec
            GetComponent<Collider2D>().enabled=false;
            GetComponent<Animator>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("DEAD !");
            GameManager.Instance.UpdateGameState(GameState.Dead);
            Destroy(gameObject, 1f);
        }
    }
}
