using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlatform : MonoBehaviour {

    [Header("Movement Parameters")]
    public float speed;
    public float size;
    public Direction startDirection;
    private int bias;
    private bool canMove = false;

    public float playerStopTime = 0.2f;
    public float playerStartTime = 0.1f;

    private Rigidbody2D myRB;
    private SpriteRenderer mySR;

    private void Start()
    {
        bias = startDirection == Direction.RIGHT ? 1 : -1;

        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        InitializeSpriteFlip();
    }
    private void FixedUpdate()
    {
        if(canMove)
            Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canMove = true;;
        }
    }
    private void Move()
    {
        if (HasWall())
        {
            bias *= -1;
        }
        myRB.velocity = new Vector2(speed * bias, 0);
    }
    private void InitializeSpriteFlip()
    {
        mySR.flipX = bias == 1;
    }
    private bool HasWall()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D ray = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            size * bias,
            LayerMask.GetMask("Ground", "Obstacles")
            );
        Physics2D.queriesStartInColliders = true;
        return ray.collider != null;
    }
}
