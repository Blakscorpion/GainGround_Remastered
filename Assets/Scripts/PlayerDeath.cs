using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject babyPrefab;
    private void OnCollisionEnter2D(Collision2D collision) {

        //If player touches the ennemy
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            HeroesManager.Instance.HeroDead();
            
            GameManager.Instance.UpdateGameState(GameState.Dead);
            GameObject newBabyTotem = Instantiate(babyPrefab, transform.position,transform.rotation);
            Destroy(gameObject);
            newBabyTotem.SendMessage("SetName", HeroesManager.Instance.CurrentHero);
        }
    }
}
