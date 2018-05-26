using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
	[Header("Run Preferences")]
	[SerializeField] public float velocity;
	[SerializeField] private float gravity;
	[Header("Jump Preferences")]
	[SerializeField] private float jumpForce;
	[SerializeField] private int jumpLimit;
	[Header("Grounded Preferences")]
	[SerializeField] private Transform groundPosition;
	[SerializeField] private Vector2 groundBoxCastSize;
	[Header("Death Animation")]
	[SerializeField] private int bloodCells;
	[SerializeField] private GameObject bloodPrefab;
	[Header("UI")]
	[SerializeField] private GameOverPanel gameOverPanel;
	[SerializeField] private StartText startText;
	
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anmt;

	[HideInInspector] public static bool canStart;
	private bool alive;
	[SerializeField] private int currentJump;
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anmt = GetComponent<Animator>();
		StartCoroutine( MovementState() );
	}
	private IEnumerator MovementState()
	{
		alive = true;
		canStart = false;
		//Wait until player give the first input.
		yield return WaitForPlayerInitialInput();
		canStart = true;
		startText.StopAnimation();
		//Movement Loop
		while(alive)
		{
			bool isGrounded = IsGrounded();
			Move();
			Jump(isGrounded);
			Gravity();
			yield return null;
		}
	}
	private void Move()
	{
		animation_Move();
		float vel = rb.velocity.x;
		rb.velocity = new Vector2(velocity,	rb.velocity.y);
	}
	private void Jump(bool isGrounded)
	{
		if(Input.GetMouseButtonDown(0) && currentJump < jumpLimit)
		{
			if(currentJump == 0 &&isGrounded || currentJump > 0)
			{
				currentJump++;
				animation_Jump();
				animation_Land(false);
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			}
		}
	}
	private void Gravity()
	{
		rb.velocity += Vector2.down * gravity * Time.deltaTime;
	}
	private bool IsGrounded()
	{
		if (rb.velocity.y > 0) return false;
		RaycastHit2D boxCast = Physics2D.BoxCast(groundPosition.position,
                groundBoxCastSize,
                0,
                Vector2.zero,
                0,
                1 << LayerMask.NameToLayer("Ground")
				);
		if (boxCast.collider != null)
		{
			animation_Land(true);
			currentJump = 0;
			return true;
		}
		return false;
	}
	private void OnCollisionEnter2D(Collision2D obj)
	{
		if(obj.gameObject.tag == "Obstacles")
		{
			Die();
		}
	}
	private void Die()
	{
		alive = false;
		for(int i = 0; i < bloodCells; i++)
		{
			var b = Instantiate(bloodPrefab,transform.position,Quaternion.identity);
			b.GetComponent<Rigidbody2D>().AddForce(Vector2.up*Random.Range(1f,2f) + Vector2.right*Random.Range(-1f,1f)*2f);
			b.transform.DOScale(0f,1f);
		}
		int deathCount = PlayerPrefs.GetInt("deathCount",0);
		PlayerPrefs.SetInt("deathCount",++deathCount);
		gameOverPanel.ShowGameOverPanel();
		Destroy(this.gameObject);
	}
	private IEnumerator WaitForPlayerInitialInput()
	{
		while(!Input.GetMouseButton(0))yield return null;
	}
	#region ANIMATION

	private void animation_Land(bool value)
    {
        anmt.SetBool("grounded", value);
    }
	private void animation_Jump()
	{
		anmt.SetTrigger ("jump");
    }
	private void animation_Move()
	{
		anmt.SetFloat ("velocity", Mathf.Abs(rb.velocity.x));
	}
	#endregion
}
