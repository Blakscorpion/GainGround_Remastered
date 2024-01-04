using System;
using UnityEngine;

public class PlayerAnimationToMouse : MonoBehaviour
{
    public float movespeed;
    public Rigidbody2D rigidBody;
    private Vector3 velocity = Vector3.zero;
    private Vector2 movement;
    private Vector2 mouseVector;

    public Animator animator;
    private SpriteRenderer _renderer;

    AnimatorClipInfo[] animatorClipinfo;
    Animator m_Animator=null;
    private string current_animation;

    void OnDisable()
    {
        animator.SetFloat("speed", 0);
        Debug.Log("DISABLED");
    }

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

        mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        mouseVector = mouseVector.normalized;

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

         // Fliping character if going left
        if (mouseVector.x >= 0){
            _renderer.flipX = false;
        }
        else{
            _renderer.flipX = true;
        }

        //Reversing animation if player is going opposite way of its looking direction
        if(_renderer.flipX == movement.x>=0){
            animator.SetFloat("Reverse", -1.0f);
        }
        else{
            animator.SetFloat("Reverse", 1.0f);
        }
        
        if (movement.x !=0 || movement.y !=0)
        {
            //animatorClipinfo = this.m_Animator.GetCurrentAnimatorClipInfo(0);
            SetLastDirection();
        }        
    }

    void FixedUpdate() {
        
    
        rigidBody.MovePosition(rigidBody.position + movement.normalized * movespeed * Time.deltaTime);
        //Vector3 targetVelocityH = new Vector2(_horizontalMovement, rigidBody.velocity.y);
        //Vector3 targetVelocityV = new Vector2(_verticalMovement, rigidBody.velocity.x);
        //rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, targetVelocityH + targetVelocityV, ref velocity, .05f);
    }

    private void SetLastDirection()
    {
        if (movement.y > 0)
        {
            current_animation = "Top";
            /* if (movement.x < 0.2f && movement.x > -0.2f){
                current_animation = "Top";}
            
            else if (movement.x > 0.2f){
                current_animation = "TopRight";}
            
            else{
                current_animation = "TopLeft";} */
        }
        
        else if (movement.y < 0)
        {
            current_animation = "Down";
            /* if (movement.x < 0.2f && movement.x > -0.2f){
                current_animation = "Down";}
            
            else if (movement.x > 0.2f){
                current_animation = "DownRight";}
            
            else{
                current_animation = "DownLeft";} */
        }

        else{
            if (movement.x > 0){
                current_animation = "Right";}
            
            else {
                current_animation = "Left";}
        }
    }

    public string GetCurrentMovement()
    {
        return current_animation;
    }
}

