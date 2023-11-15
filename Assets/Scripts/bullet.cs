using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damagePerHit = 1;
    public GameObject hitEffect;
    public int recoilStrenght=0;
    public AudioClip soundShoot;
    public AudioClip soundContact;

    void Awake()
    {
        SoundManager.Instance.PlaySoundFX(soundShoot);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
   {
        SoundManager.Instance.PlaySoundFX(soundContact);
        GameObject effect = Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
        Destroy(effect, 2.0f);
        Destroy(gameObject);
   }
}
