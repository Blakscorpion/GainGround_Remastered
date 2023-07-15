using UnityEngine;
using System.Collections;

public class shootingFreeAim : MonoBehaviour
{
    public Transform firePoint;
    public GameObject primaryAmmo;
    public GameObject secondaryAmmo;
    private int lastDirection;
    SpriteRenderer sprite;
    public float bulletForce = 20f;
    public float shootingInterval=0.2f;
    public float shootingIntervalSpecial=2f;

    private bool isAbleToShoot=true;
    private bool isAbleToShootSpecial=true;


    private void Start() {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1") && isAbleToShoot)
        {
            isAbleToShoot=false;
            Shoot(false);
            StartCoroutine(WaitAndShootAgain(shootingInterval));
        }
        if(Input.GetButtonDown("Fire2") && isAbleToShootSpecial)
        {
            isAbleToShootSpecial=false;
            Shoot(true);
            StartCoroutine(WaitAndShootSpecialAgain(shootingIntervalSpecial));
        }
    }

    void Shoot(bool isSpecialAttack)
    {
        // Si isSpecialAttack == true, on utilise l'attaque spéciale, sinon l'attaque principale
        GameObject bullet = Instantiate(isSpecialAttack?secondaryAmmo:primaryAmmo, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                
        // Rotate the amo sprite, to be in the direction of the shooting 
        Vector3 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        float angle = (Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg) - 90;
        rb.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        //Shoot in the direction of the aim
        Vector2 dir2D = new Vector2(dir.x, dir.y);
        rb.AddForce(dir2D.normalized * bulletForce , ForceMode2D.Impulse);

        Destroy(bullet, 6f);
    }

    // wait X seconds to be able to attack again
    private IEnumerator WaitAndShootAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isAbleToShoot=true;
    }

    // wait X seconds to be able to use special attack again
    private IEnumerator WaitAndShootSpecialAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isAbleToShootSpecial=true;
    }

}
