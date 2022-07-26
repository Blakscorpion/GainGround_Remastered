using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyController : MonoBehaviour
{
    GameObject player;
    public Animator animator;
    bool isBabyCollected = false;
    SpriteRenderer RendererComponent;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBabyCollected)
        {
            if (player)
            {
                string currMov = player.GetComponent<PlayerMovement>().GetCurrentMovement();
                BabyPlacement(currMov); 
            
                // Animating the Baby when player is moving
                if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
                {
                    animator.SetBool("isMoving", true);
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical"); 

                animator.SetFloat("horizontal", movement.x);
                animator.SetFloat("vertical", movement.y);
                }
                else 
                {
                    animator.SetBool("isMoving", false);
                }
            }

            else
            {
                isBabyCollected=false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && isBabyCollected==false)
        {
            Debug.Log("Baby gathered");
            isBabyCollected=true;
            player = other.gameObject;
            this.transform.position = player.transform.position;

            // On récup le spriterender du player pour savoir s'il est flipX ou pas, car le nom de l'animation est dans les 2 cas == MoveRight.
            //Donc faut savoir si elle est à droite + flipX ou à droite pas flipX
            RendererComponent = player.GetComponent<SpriteRenderer>();
        }
    }

    private void BabyPlacement(string currMov)
    {
        switch (currMov)
        {
            case "Top": // put baby down
                this.transform.position = player.transform.GetChild(1).GetChild(1).transform.position;
                break;
            case "TopLeft":
                this.transform.position = player.transform.GetChild(1).GetChild(6).transform.position;
                break;
            case "TopRight":
                if (RendererComponent.flipX)
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(6).transform.position;
                }
                else
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(7).transform.position;
                }
                break;
            case "Down":
                this.transform.position = player.transform.GetChild(1).GetChild(0).transform.position;
                break;
            case "DownRight":
                if (RendererComponent.flipX)
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(4).transform.position;
                }
                else
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(5).transform.position;
                }
                break;
            case "DownLeft":
                this.transform.position = player.transform.GetChild(1).GetChild(4).transform.position;
                break;
            case "Right":
                if (RendererComponent.flipX)
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(3).transform.position;
                }
                else
                {
                    this.transform.position = player.transform.GetChild(1).GetChild(2).transform.position;
                }
                break;
            case "Left":
                this.transform.position = player.transform.GetChild(1).GetChild(3).transform.position;
                break;
            default: //  séquence d’instructions par défaut
                break;
        }
    }

    
}
