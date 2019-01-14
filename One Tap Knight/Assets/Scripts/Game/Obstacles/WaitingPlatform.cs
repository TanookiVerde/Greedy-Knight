using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPlatform : MonoBehaviour
{
    [Header("Preferences")]
    public float velocity;

    private int directionBias = 1;
    private new Rigidbody2D rigidbody2D;
    private bool initialized;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(initialized)
            Move();
    }
    private void Move()
    {
        rigidbody2D.velocity = new Vector2(velocity * directionBias, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            initialized = true;
            collision.gameObject.GetComponent<KnightController>().ModifyVelocity(0);
        }
        else if (!collision.gameObject.CompareTag("Player"))
            directionBias *= 0;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            initialized = false;
            collision.gameObject.GetComponent<KnightController>().ModifyVelocity(1);
        }

    }
}
