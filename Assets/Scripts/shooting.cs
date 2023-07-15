using UnityEngine;
using System.Collections;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject primaryAmmo;
    public GameObject secondaryAmmo;
    Animator m_Animator=null;
    AnimatorClipInfo[] animatorinfo=null;
    private string current_animation="";
    private int lastDirection;
    SpriteRenderer sprite;
    public float bulletForce = 20f;
    public int shootingInterval=1;
    public int shootingIntervalSpecial=2;

    private bool isAbleToShoot=true;
    private bool isAbleToShootSpecial=true;


    private void Start() {
        m_Animator = gameObject.GetComponent<Animator>();
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
        
        animatorinfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;
        lastDirection = m_Animator.GetInteger("LastDirection");
        // We check the direction of the hero : 1=TOP 2=TOPRIGHT 3=RIGHT 4=BOTTOMRIGHT... 8=TOPLEFT
        
        // Shooting down
        if(lastDirection == 5)
        {
            rb.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            rb.AddForce(-firePoint.up * bulletForce , ForceMode2D.Impulse);
        }
        // Shooting sideway
        else if (lastDirection == 3)
        {   
            rb.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            rb.AddForce(firePoint.right * bulletForce , ForceMode2D.Impulse);
        }
        else if (lastDirection == 7)
        {   
            rb.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            rb.AddForce(-firePoint.right * bulletForce , ForceMode2D.Impulse);
        }
        // Shooting up
        else if (lastDirection == 1)
        {  
            rb.AddForce(firePoint.up * bulletForce , ForceMode2D.Impulse);
        }
        else if (lastDirection == 2)
        {   
            rb.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);

            rb.AddForce(new Vector2(1,1) * bulletForce, ForceMode2D.Impulse);
        }
        else if (lastDirection == 4)
        {   
            rb.transform.Rotate(0.0f, 0.0f, -135.0f, Space.Self);
            rb.AddForce(new Vector2(1,-1) * bulletForce , ForceMode2D.Impulse);
        }
        else if (lastDirection == 8)
        {   
            rb.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);

            rb.AddForce(new Vector2(-1,1) * bulletForce, ForceMode2D.Impulse);
        }
        else if (lastDirection == 6)
        {   
            rb.transform.Rotate(0.0f, 0.0f, 135.0f, Space.Self);
            rb.AddForce(new Vector2(-1,-1) * bulletForce , ForceMode2D.Impulse);
        }

        Destroy(bullet, 6f);
    }

    // wait X seconds to be able to attack again
    private IEnumerator WaitAndShootAgain(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isAbleToShoot=true;
    }

    // wait X seconds to be able to use special attack again
    private IEnumerator WaitAndShootSpecialAgain(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isAbleToShootSpecial=true;
    }

}
