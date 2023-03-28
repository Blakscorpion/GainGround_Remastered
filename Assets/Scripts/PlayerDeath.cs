using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject babyPrefab;
    private void OnCollisionEnter2D(Collision2D collision) {

        //If player touches the ennemy
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            
            //Freeze player, then delete after 1sec
            GetComponent<Collider2D>().enabled=false;
            GetComponent<Animator>().enabled = false;
            
            GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("DEAD !");
            Destroy(gameObject, 0.5f);
            GameObject newBabyTotem = Instantiate(babyPrefab, transform.position,transform.rotation);
            newBabyTotem.SendMessage("SetName", HeroesManager.Instance.CurrentHero);
            GameManager.Instance.UpdateGameState(GameState.Dead);
        }
    }
}
