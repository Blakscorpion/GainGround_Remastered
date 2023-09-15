using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movement;
    private bool isDashing = false;
    [SerializeField]
    private float dashForce = 1;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // When pressing action button, dash to the mouse direction
        if(Input.GetButtonDown("Jump") && !isDashing)
        {
            isDashing=true;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            DashToPlayerDirection();
        }

        isDashing=false;
    }

    private void DashToPlayerDirection()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement=movement.normalized;
	    rb.AddForce(movement * dashForce * 1000);
    }
}
