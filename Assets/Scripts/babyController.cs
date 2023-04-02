using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyController : MonoBehaviour
{
    GameObject player;
    babyInfo babyInfo;
    PlayerStats playerStats;
    public Animator animator;
    bool isBabyCollected = false;
    SpriteRenderer RendererComponent;
    private Vector2 movement;
    private int currentBabyIteration =0;

     private void Awake() {
        GameManager.OnGameStateChanged += CheckBabyNumberAndDestroy;
        GameManager.OnGameStateChanged += RemoveBabyFromStageWhenExit;
    }

    private void OnDestroy() {
        GameManager.OnGameStateChanged -= CheckBabyNumberAndDestroy;
        GameManager.OnGameStateChanged += RemoveBabyFromStageWhenExit;
    }

    // Check if not exceeding the maximum number of babies allowed. If yes, kill (only the baby from dead heroes, not the level baby --> tag == baby and not BabyFromLevel)
    private void CheckBabyNumberAndDestroy(GameState state) {
        if(state == GameState.Dead && this.tag=="Baby")
        {   
            currentBabyIteration++;
            Debug.Log("There are currently " + GameObject.FindGameObjectsWithTag("Baby").Length + " baby.");
            // If there are more babies on the level than the number allowed, and if the idNumber of this one is higher than expected, we destroy it
            if (currentBabyIteration > LevelManager.Instance.NumberOfBabiesAllowed && GameObject.FindGameObjectsWithTag("Baby").Length > LevelManager.Instance.NumberOfBabiesAllowed)
            {   
                // TODO : Destroy only the last one
                GameObject.Destroy(this.gameObject);
            }
        }
    }

    // If Hero went to exit with a baby, remove the baby from the scene, as it's considered as passed also
    private void RemoveBabyFromStageWhenExit(GameState state) {
        if(state == GameState.ExitSuccess && isBabyCollected)
        {   
            isBabyCollected=false;
            Debug.Log("Baby " + babyInfo.babyHeroName + " has also passed with the hero");
            {   
                // TODO : Destroy the baby gameobject
                GameObject.Destroy(this.gameObject);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        babyInfo = GetComponent<babyInfo>();
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
            player = other.gameObject;
            playerStats = player.GetComponent<PlayerStats>();
            
            if (playerStats.hasABaby == false)
            {
                Debug.Log("Baby gathered");
                isBabyCollected=true;
                playerStats.hasABaby=true;
                playerStats.babyFollowing =  babyInfo.babyHeroName;
                
                this.transform.position = player.transform.position;

                // On récup le spriterender du player pour savoir s'il est flipX ou pas, car le nom de l'animation est dans les 2 cas == MoveRight.
                //Donc faut savoir si elle est à droite + flipX ou à droite pas flipX
                RendererComponent = player.GetComponent<SpriteRenderer>();
            }
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
