using UnityEngine;
using System.Collections;

public class shootingFreeAim : MonoBehaviour
{
    public Transform firePoint;
    public GameObject primaryAmmo;
    public float recoilTimeSlow=0.2f;
    private int recoilForcePrimary;
    private bool recPrim=false;
    private bool recSec=false;
    public GameObject secondaryAmmo;
    private int recoilForceSecundary;
    private int lastDirection;
    SpriteRenderer sprite;
    Rigidbody2D rigidbdy;
    public float bulletForce = 20f;
    public float shootingInterval=0.2f;
    public float shootingIntervalSpecial=2f;
    private bool isAbleToShoot=true;
    private bool isAbleToShootSpecial=true;
    private PlayerAnimationToMouse playerMovement;
    private Vector3 dir;
    private float angle;
    public GameObject rb_Weapon;
    public SpriteRenderer WeaponRenderer;


    private void Start() {
        sprite = this.GetComponent<SpriteRenderer>();
        rigidbdy = this.GetComponent<Rigidbody2D>();
        playerMovement = this.GetComponent<PlayerAnimationToMouse>();
        recoilForcePrimary = primaryAmmo.GetComponent<bullet>().recoilStrenght;
        recoilForceSecundary = secondaryAmmo.GetComponent<bullet>().recoilStrenght;
        WeaponRenderer = rb_Weapon.GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Fire1") && isAbleToShoot)
        {
            isAbleToShoot=false;
            Shoot(false);
            recPrim=true;
            StartCoroutine(WaitAndShootAgain(shootingInterval));
        }
        if(Input.GetButtonDown("Fire2") && isAbleToShootSpecial)
        {
            isAbleToShootSpecial=false;
            Shoot(true);
            recSec=true;
            StartCoroutine(WaitAndShootSpecialAgain(shootingIntervalSpecial));
            StartCoroutine(SlowingPlayerCoroutine());
        }

        dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = (Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg) - 90;
        Debug.Log(angle);
        if (rb_Weapon != null){
            if (angle > 0 || angle < -181)
            {
                WeaponRenderer.flipY = true;
            }
            else{
                WeaponRenderer.flipY = false;
            }
            rb_Weapon.transform.rotation = Quaternion.AngleAxis(angle+100, Vector3.forward);
        }
    }

    void FixedUpdate()
    {
        if (recPrim==true){applyRecoilForce(1);recPrim=false;}
        if (recSec==true){applyRecoilForce(2);recSec=false;}
    }

    void Shoot(bool isSpecialAttack)
    {
        // Si isSpecialAttack == true, on utilise l'attaque spéciale, sinon l'attaque principale
        GameObject bullet = Instantiate(isSpecialAttack?secondaryAmmo:primaryAmmo, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                
        // Rotate the amo sprite, to be in the direction of the shooting 
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

    private void applyRecoilForce(int primaryOrSecondaryAmmo)
    {
        Vector2 mouseVectorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    Vector2 playerVectorPosition = transform.position;
	    Vector2 ForceVector = (mouseVectorPosition - playerVectorPosition).normalized;
	    if (primaryOrSecondaryAmmo==1)
        {
            rigidbdy.AddForce(ForceVector * -1 * recoilForcePrimary*100);
            Debug.Log("Recoil de force : " + recoilForcePrimary);
        }
        else{
            rigidbdy.AddForce(ForceVector * -1 * recoilForceSecundary*100);
            Debug.Log("Recoil de force : " + recoilForceSecundary);
        }
    }

    IEnumerator SlowingPlayerCoroutine()
    {
        float oldMovSpeed = playerMovement.movespeed;
        playerMovement.movespeed = oldMovSpeed /3;
        yield return new WaitForSeconds(recoilTimeSlow);
        playerMovement.movespeed = oldMovSpeed;
    }
}
