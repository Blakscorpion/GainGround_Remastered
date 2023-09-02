using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damagePerHit = 1;
    public GameObject hitEffect;
    public int recoilStrenght=0;


    private void OnCollisionEnter2D(Collision2D collision) 
   {
       GameObject effect = Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
       Destroy(effect, 1f);
       Destroy(gameObject);
   }
}
