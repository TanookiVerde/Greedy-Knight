using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformCharater : MonoBehaviour {
	[Header("Movement Preferences")]
	public float velocity;
	public int jumpLimit;
	public float gravity;
	public float jumpMin;
	public float jumpMaxTime;
	public float jumpIncreaseTax;

	[Header("Grounded Preferences")]
	public Transform groundPosition;
	public Vector2 groundBoxCastSize;

	private float horDir;

	[Header("Components")]
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anmt;

	private void Start()
	{
		GetRigidbody ();
		GetSpriteRenderer ();
		GetAnimator ();
	}
	private void Update()
	{
		Move (GetInputDirection ());
		GravityEffect ();
		Jump ();
		FlipSprite ();
	}
	private void GetRigidbody()
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	private void GetSpriteRenderer()
	{
		sr = GetComponent<SpriteRenderer> ();
	}
	private void GetAnimator()
	{
		anmt = GetComponent<Animator> ();
	}
	private void Move(Vector2 direction)
	{
		animation_Move ();
		var vel = rb.velocity;
		vel.x = direction.x*velocity;
		if (vel.x != 0)
			horDir = vel.x;
		rb.velocity = vel;
	}
	private Vector2 GetInputDirection()
	{
		return new Vector2 (Input.GetAxisRaw ("Horizontal"), 0);
	}
	private void GravityEffect()
	{
		rb.velocity += Vector2.down * gravity * Time.deltaTime;
	}
	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && IsGrounded ()) {
			animation_Jump ();
			StartCoroutine (SpecialJump ());
		}
	}
	private IEnumerator SpecialJump()
	{
		float time = jumpMaxTime;
		rb.velocity += Vector2.up * jumpMin;
		while (Input.GetKey (KeyCode.Space) && time > 0) {
			yield return new WaitForEndOfFrame ();
			rb.velocity += Vector2.up * jumpIncreaseTax;
			time -= Time.deltaTime;
		}
	}
	private bool IsGrounded()
	{
		RaycastHit2D boxCast = Physics2D.BoxCast (groundPosition.position,
			groundBoxCastSize,
			0, 
			Vector2.zero,
			0,
			1 << LayerMask.NameToLayer("Ground"));
		return boxCast.collider != null;
	}
	private void FlipSprite()
	{
		if (horDir < 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}
	}
	private void animation_Jump()
	{
		anmt.SetTrigger ("jump");
	}
	private void animation_Move()
	{
		anmt.SetFloat ("velocity", Mathf.Abs(rb.velocity.x));
	}
}
