using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    [Header("Preferences")]
    public float velocity;
    public float viewDistance;

    private int directionBias = 1;
    private new Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().flipX = directionBias == 1;
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rigidbody2D.velocity = new Vector2(velocity * directionBias, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<KnightController>().Die();
        else
            Invert();
    }
    private void Invert()
    {
        directionBias *= -1;
        GetComponent<SpriteRenderer>().flipX = directionBias == 1;
    }
}
