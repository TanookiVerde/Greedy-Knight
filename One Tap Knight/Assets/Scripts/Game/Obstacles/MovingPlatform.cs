using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Preferences")]
    public float velocity;
    public float screwRotationVelocity;

    private int directionBias = 1;
    private new Rigidbody2D rigidbody2D;
    private Transform screw;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        screw = transform.GetChild(0);
    }
    private void Update()
    {
        RotateScrew();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rigidbody2D.velocity = new Vector2(velocity * directionBias, 0);
    }
    private void RotateScrew()
    {
        screw.Rotate(new Vector3(0, 0, screwRotationVelocity*directionBias));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            directionBias *= -1;
        else
            collision.gameObject.GetComponent<KnightController>().FollowX(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<KnightController>().StopFollowing();
    }
}
