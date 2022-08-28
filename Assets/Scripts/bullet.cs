using UnityEngine;

public class bullet : MonoBehaviour
{
     public int damagePerHit = 1;
    public GameObject hitEffect;
   private void OnCollisionEnter2D(Collision2D collision) 
   {
       GameObject effect = Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
       Destroy(effect, 0.5f);
       Destroy(gameObject);
   }
}
