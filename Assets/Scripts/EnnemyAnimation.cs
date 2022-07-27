using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyAnimation : MonoBehaviour
{
 

    private Vector2 movement;
    public Animator animator;
    private Vector2 oldPosition;
    private Vector2 newPosition;

    void Start() {
        oldPosition = transform.position;
    }

    void FixedUpdate()
    {
        newPosition = transform.position;
        movement.x = newPosition.x - oldPosition.x;
        movement.y = newPosition.y - oldPosition.y;
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        oldPosition = newPosition;
    }
}
