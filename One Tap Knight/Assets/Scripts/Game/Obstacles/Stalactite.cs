using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stalactite : MonoBehaviour {
    private State state;
    public float distanceToShake;
    public float distanceToFall;
    public float finalGravityScale;
    public float finalDownForce;

    private KnightController knight;
    private Animator animator;
    private new Rigidbody2D rigidbody2D;

	private void Start () {
        knight = FindObjectOfType<KnightController>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	private void Update () {
        if(knight != null)
        {
            float distance = (knight.transform.position - transform.position).x;
            distance = Mathf.Abs(distance);
            if (state == State.SHAKING && distance < distanceToFall)
                Fall();
            else if(state == State.IDLE && distance < distanceToShake)
                Shake();
        }
	}
    private void Shake()
    {
        animator.Play("shake");
        state = State.SHAKING;
    }
    private void Fall()
    {
        animator.Play("idle");
        state = State.FALLING;
        rigidbody2D.AddForce(Vector2.down * finalDownForce);
        rigidbody2D.gravityScale = finalGravityScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<KnightController>().Die();
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(rigidbody2D);
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
    public enum State
    {
        IDLE, SHAKING, FALLING
    }
}
