using UnityEngine;

public class EnnemyLifeManager : MonoBehaviour
{   
    public int startingHP = 1;
    private int remainingHP;
    private AudioSource audiosource;
    

    private void Start() {
       remainingHP = startingHP;
       audiosource = GetComponent<AudioSource>();
   }
   
   private void OnCollisionEnter2D(Collision2D collision) {
       if(collision.gameObject.CompareTag("damagingAmmo"))
       {
           int damageOfAmmo = collision.gameObject.GetComponent<bullet>().damagePerHit;
           Debug.Log("HP before beiing hit : " + remainingHP);
           remainingHP -= damageOfAmmo;
           Debug.Log("HP after beiing hit : " + remainingHP);

           if (remainingHP <= 0)
           {
                audiosource.Play();
                LevelManager.Instance.RemoveEnnemy();
                GetComponent<SpriteRenderer>().enabled=false;
                GetComponent<BoxCollider2D>().enabled=false;
                Destroy(this.gameObject, 2.0f);
           }
       }
   }
}
