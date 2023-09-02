using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject babyPrefab;
    private AudioSource audioSource;
    public AudioClip audioClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        //If player touches the ennemy
        if(collision.gameObject.CompareTag("Ennemy"))
        {
            playSoundDead();
            HeroesManager.Instance.HeroDead();
            
            GameManager.Instance.UpdateGameState(GameState.Dead);
            GameObject newBabyTotem = Instantiate(babyPrefab, transform.position,transform.rotation);
            Destroy(gameObject);
            newBabyTotem.SendMessage("SetName", HeroesManager.Instance.CurrentHero);
        }
    }

    //Use SoundManager instead of audioSource when you destroy the gameobject on which the audio source is
    //Which causes to not play the sound as it's destroyed.
    private void playSoundDead()
    {
        SoundManager.Instance.PlaySound(audioClip);
    }
}
