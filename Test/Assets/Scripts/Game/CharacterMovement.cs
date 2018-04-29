using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour {
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
	private float bias = 1;

	[Header("Components")]
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anmt;

	private bool alive = true;

	private void Start()
	{
		GetRigidbody ();
		GetSpriteRenderer ();
		GetAnimator ();
		StartCoroutine(PlayerControllerStates());
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
	}
	private IEnumerator PlayerControllerStates(){
		alive = true;
		yield return WaitForPlayerInitialInput();
		FindObjectOfType<StartText>().StopAnimation();
		while(alive){
			Move (Vector2.right*bias);
			Jump ();
			FlipSprite ();
			GravityEffect ();
			yield return null;
		}
		while(true){
			GravityEffect ();
			yield return null;
		}
	}
	private IEnumerator WaitForPlayerInitialInput(){
		while(!Input.GetMouseButton(0)){
			yield return null;
		}
	}
	private void GetRigidbody()
	{
		rb = GetComponent<Rigidbody2D> ();
	}
	private void InvertBias(){
		bias = -bias;
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
		if (Input.GetMouseButtonDown(0) && IsGrounded ()) {
			animation_Jump ();
			StartCoroutine (SpecialJump ());
		}
	}
	private IEnumerator SpecialJump()
	{
		float time = jumpMaxTime;
		rb.velocity += Vector2.up * jumpMin;
		while (Input.GetMouseButton(0) && time > 0) {
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
	private bool HasWall()
	{
		RaycastHit2D boxCast = Physics2D.BoxCast (transform.position,
			groundBoxCastSize*3f,
			0, 
			Vector2.zero,
			0,
			1 << LayerMask.NameToLayer("Ground"));
		return boxCast.collider != null;
	}
	private void ChangeDirection(){
		if(HasWall()){
			InvertBias();
		}
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
	public void NotAlive(){
		alive = false;
	}
}
