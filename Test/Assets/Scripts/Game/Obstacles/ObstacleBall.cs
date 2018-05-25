using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBall : MonoBehaviour {

    [Header("Movement Parameters")]
    public float movementSpeed;

    [Header("Grounded Preferences")]
    public Transform groundPosition;
    public Vector2 groundBoxCastSize;

    bool grounded = true;

    private Rigidbody2D myRB;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        IsGrounded();
        Debug.Log(grounded);
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
            movementSpeed = -movementSpeed;
    }
    private void IsGrounded()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(groundPosition.position,
            groundBoxCastSize,
            0,
            Vector2.zero,
            0,
            1 << LayerMask.NameToLayer("Ground"));
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
