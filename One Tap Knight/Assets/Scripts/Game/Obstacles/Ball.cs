using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float movementSpeed;

    [Header("Grounded Preferences")]
    [SerializeField] private Transform groundPosition;
    [SerializeField] private Vector2 groundBoxCastSize;

    private bool grounded = true;

    private Rigidbody2D myRB;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        IsGrounded();
        if (grounded)
        {
            myRB.velocity = new Vector2(movementSpeed, 0);
        }
        else
        {
            myRB.velocity = new Vector2(0, -Mathf.Abs(movementSpeed));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground" && grounded)
        {
            movementSpeed = -movementSpeed;
        }
    }
    private void IsGrounded()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(groundPosition.position,
            groundBoxCastSize,
            0,
            Vector2.zero,
            0,
            1 << LayerMask.NameToLayer("Ground")
            );
        if (boxCast.collider != null)
        {
            grounded = true;
            return;
        }
        else
        {
            grounded = false;
        }
        
    }
}
