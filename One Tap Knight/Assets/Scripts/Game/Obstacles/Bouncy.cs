using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy : MonoBehaviour {

    [Header("Movement Parameters")]
    public float speed;
    public float size;
    public int bias = -1;
    [Tooltip("If bounceForce = 0 the bounce will use the same force as a jump.")]
    public float bounceForce = 0;

    private Rigidbody2D myRB;
    private SpriteRenderer mySR;

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        InitializeSpriteFlip();
    }
    private void FixedUpdate()
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(bounceForce == 0)
                collision.gameObject.GetComponent<Character>().Bounce();
            else
                collision.gameObject.GetComponent<Character>().Bounce(bounceForce);
        }
    }
}
