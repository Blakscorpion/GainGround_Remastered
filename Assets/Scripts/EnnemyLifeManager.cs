using UnityEngine;

public class EnnemyLifeManager : MonoBehaviour
{   
    public int startingHP = 1;
    private int remainingHP;
    

    private void Start() {
       remainingHP = startingHP;
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
               LevelManager.Instance.RemoveEnnemy();
               Destroy(this.gameObject);
           }
       }
   }
}
