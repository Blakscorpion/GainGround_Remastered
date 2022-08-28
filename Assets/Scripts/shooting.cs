using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject primaryAmmo;
    public GameObject secondaryAmmo;
    Animator m_Animator=null;
    AnimatorClipInfo[] animatorinfo=null;
    private string current_animation="";
    SpriteRenderer sprite;
    public float bulletForce = 20f;

    private void Start() {
        m_Animator = gameObject.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
         Shoot(false);
        }
        if(Input.GetButtonDown("Fire2"))
        {
         Shoot(true);
        }
    }

    void Shoot(bool isSpecialAttack)
    {
        // Si isSpecialAttack == true, on utilise l'attaque spéciale, sinon l'attaque principale
        GameObject bullet = Instantiate(isSpecialAttack?secondaryAmmo:primaryAmmo, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        animatorinfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
        current_animation = animatorinfo[0].clip.name;

        // Shooting down
        if( current_animation == "BettyFront")
        {
            Debug.Log("Shoot Down");
            rb.AddForce(-firePoint.up * bulletForce , ForceMode2D.Impulse);
        }
        // Shooting sideway
        else if (current_animation == "BettyProfil")
        {   
            rb.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            // if going left
            if (sprite.flipX)
            {
                Debug.Log("Shoot left");
                rb.AddForce(-firePoint.right * bulletForce , ForceMode2D.Impulse);
            }
            // If going right
            else{
                Debug.Log("Shoot right");                
                rb.AddForce(firePoint.right * bulletForce , ForceMode2D.Impulse);
            }
        }
        // Shooting up
        else
        {  
            Debug.Log("Shoot up");
            rb.AddForce(firePoint.up * bulletForce , ForceMode2D.Impulse);
        }

        Destroy(bullet, 8f);
    }

}
