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
            GameObject newBabyTotem = Instantiate(babyPrefab, transform.position,transform.rotation);
            Destroy(gameObject);
            newBabyTotem.SendMessage("SetName", HeroesManager.Instance.CurrentHero);
            DialogueManager.Instance.CheckDeathDialogue(HeroesManager.Instance.CurrentHero);
            GameManager.Instance.UpdateGameState(GameState.Dead);
        }
    }

    //Use SoundManager instead of audioSource when you destroy the gameobject on which the audio source is
    //Which causes to not play the sound as it's destroyed.
    private void playSoundDead()
    {
        SoundManager.Instance.PlaySound(audioClip);
    }
}
