using UnityEngine;

public class bullet : MonoBehaviour
{
     public int damagePerHit = 1;
    public GameObject hitEffect;
   private void OnCollisionEnter2D(Collision2D collision) 
   {
       GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
       Destroy(effect, 0.5f);
       Destroy(gameObject);
   }
}
