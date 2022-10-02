using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    public Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;
    private Vector2 movement;
    public Animator animator;
    private SpriteRenderer _renderer;

    AnimatorClipInfo[] animatorClipinfo;
    Animator m_Animator=null;
    private string current_animation;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }
        m_Animator = gameObject.GetComponent<Animator>();
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

        //Setting Last Direction of the Hero : 1=TOP 2=TOPRIGHT 3=RIGHT 4=BOTTOMRIGHT... 8=TOPLEFT
        
        
        if (movement.x !=0 || movement.y !=0)
        {
            animatorClipinfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
            SetLastDirection();
        }        
    }

    void FixedUpdate() {
        
    
        rigidBody.MovePosition(rigidBody.position + movement * movespeed *Time.deltaTime);
        //Vector3 targetVelocityH = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        //Vector3 targetVelocityV = new Vector2(_verticalMovement, rigidBody.velocity.x);
        //rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocityH + targetVelocityV, ref velocity, .05f);
    }

    private void SetLastDirection()
    {
        current_animation = animatorClipinfo[0].clip.name;

        if (current_animation =="Top")
        {
            animator.SetInteger("LastDirection", 1);
        }
        
        else if (current_animation == "Down")
        {
            animator.SetInteger("LastDirection", 5);
        }
        
        else if (current_animation == "Right")
        {
            if (_renderer.flipX)
            {
                animator.SetInteger("LastDirection", 7);
            }
            else
            {
                animator.SetInteger("LastDirection", 3);
            }
        }

        else if (current_animation == "TopRight")
        {
            if (_renderer.flipX)
            {
                animator.SetInteger("LastDirection", 8);
            }
            else
            {
                animator.SetInteger("LastDirection", 2);
            }
        }

        else if (current_animation == "DownRight")
        {
            if (_renderer.flipX)
            {
                animator.SetInteger("LastDirection", 6);
            }
            else
            {
                animator.SetInteger("LastDirection", 4);
            }
        }
    Debug.Log(current_animation);
    }

    public string GetCurrentMovement()
    {
        return current_animation;
    }
}
