using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;
    private Vector2 movement;
    public Animator animator;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical"); 
        
        // Fliping character if going left
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            _renderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _renderer.flipX = true;
        }   

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        
    }

    void FixedUpdate() {
        
    
        rigidBody.MovePosition(rigidBody.position + movement * movespeed *Time.deltaTime);
        //Vector3 targetVelocityH = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        //Vector3 targetVelocityV = new Vector2(_verticalMovement, rigidBody.velocity.x);
        //rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocityH + targetVelocityV, ref velocity, .05f);
    }
}
