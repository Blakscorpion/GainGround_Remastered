using UnityEngine;

public class EnnemyAnimation : MonoBehaviour
{
 

    private Vector2 movement;
    public Animator animator;
    private Vector2 oldPosition;
    private Vector2 newPosition;
    private SpriteRenderer spriteRender;
    void Start() {
        oldPosition = transform.position;
        spriteRender = transform.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        newPosition = transform.position;
        movement.x = newPosition.x - oldPosition.x;
        movement.y = newPosition.y - oldPosition.y;

        // Fliping character if going left
        if (movement.x > 0)
        {
            spriteRender.flipX = false;
        }   
        else
        {
            spriteRender.flipX = true;
        }

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        oldPosition = newPosition;
    }
}
