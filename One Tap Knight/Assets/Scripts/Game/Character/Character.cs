using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Character : MonoBehaviour
{
    private float gravityBias = 1;
	[Header("Run")]
	[SerializeField] public float velocity;
	[SerializeField] private float gravity;
    [SerializeField] public float timeToFinish = 0.5f;
    [SerializeField] public static float idealVelocity;
	[Header("Jump")]
	[SerializeField] private int jumpLimit;
	[SerializeField] private float jumpForce;
	private int currentJump;
    [Header("Grounded")]
	[SerializeField] private Transform groundPosition;
	[SerializeField] private Vector2 groundBoxCastSize;
    [Header("Ground Pound")]
    public float groundPoundDelay = 0;
    public float poundForce = 24;
    public bool canPlayerPound = false;
    private bool pounding = false;
    [Header("Components")]
    private Rigidbody2D rb;
	private SpriteRenderer sr;
    private CharacterAnimation cAnim;

	public static int coinInLevel;
	public static Transform myTransform;

    private bool finished;

    private bool stopped = false;
    private Transform followTransform;
    private float followXOffset;
    private bool following = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundPosition.position, groundBoxCastSize);
    }
    private void Start()
	{
        cAnim = GetComponent<CharacterAnimation>();
		Character.idealVelocity = velocity;
		Character.myTransform = transform;
		rb = GetComponent<Rigidbody2D>();
	}
    private void OnCollisionEnter2D(Collision2D obj)
	{
		if(obj.gameObject.tag == "Obstacles")
		{
			Die();
		}
	}
	private void OnTriggerExit2D(Collider2D obj)
	{
		if(obj.gameObject.tag == "End")
		{
            finished = true;
		}
	}
    public void Bounce()
    {
        if (pounding)
            pounding = false;
        rb.velocity = new Vector2(rb.velocity.x, -jumpForce * gravityBias);
    }
    public void Bounce(float bounceForce)
    {
        if (pounding)
            pounding = false;
        rb.velocity = new Vector2(rb.velocity.x, -bounceForce * gravityBias);
    }
	public void Action()
    { 
		Move();
        Jump(IsGrounded());
		Gravity();
	}
    public bool FinishedLevel()
    {
        return finished;
    }
	private void Move()
	{
        Debug.Log(stopped);
        cAnim.Move();
        if (!pounding && !stopped)
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        else if (following)
        {
            transform.position = new Vector3(followTransform.position.x - followXOffset, transform.position.y, transform.position.z);
        }
	}
	private void Jump(bool isGrounded)
	{
		if(Input.GetMouseButtonDown(0))
		{
            if (following)
            {
                following = false;
                stopped = false;
            }
            if (currentJump < jumpLimit)
            {
                if ((currentJump == 0 && isGrounded) || currentJump > 0)
                {
                    currentJump++;
                    cAnim.Jump();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce * gravityBias);
                }
            }
            else if (!pounding && canPlayerPound)
            {
                StartCoroutine(GroundPound());
            }
		}
	}
    private IEnumerator GroundPound()
    {
        pounding = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(groundPoundDelay);
        rb.velocity = new Vector2(rb.velocity.x, -poundForce);
    }
	private void Die()
	{
        cAnim.Die();
		PlayerPrefs.SetInt("deathCount", PlayerPrefs.GetInt("deathCount", 0)+1);
		Destroy(this.gameObject);
	}
    private void Gravity()
	{
        if (!pounding)
        {
            rb.velocity += Vector2.down * gravity * gravityBias * Time.deltaTime;
        }
	}
    public void Stop()
    {
        Debug.Log("stop");
        stopped = true;
        DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(0,0), timeToFinish);
    }
    public void Stop(float time)
    {
        Debug.Log("stop");
        stopped = true;
        DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(0, 0), time);
    }
    public void FollowX(Transform t)
    {
        followTransform = t;
        followXOffset = followTransform.position.x - transform.position.x;
        rb.velocity = Vector2.zero;
        following = true;
    }
    public void Restart()
    {
        Debug.Log("start");
        DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(velocity, 0), timeToFinish);
        stopped = false;
    }
    public void Restart(float time)
    {
        Debug.Log("start");
        DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(velocity, 0), time);
        stopped = false;
    }
    private bool IsGrounded()
	{
        if (rb.velocity.y*gravityBias > 0) return false;
        RaycastHit2D boxCast = Physics2D.BoxCast(groundPosition.position,
                groundBoxCastSize,
                0,
                Vector2.up,
                0,
                LayerMask.GetMask("Ground", "Stop")
				);

		if (boxCast.collider != null)
		{
            if (boxCast.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                cAnim.Land();
                currentJump = 0;

                if (pounding)
                    pounding = false;
            }
            if(boxCast.collider.gameObject.layer == LayerMask.NameToLayer("Stop") && !stopped)
            {
                cAnim.Land();
                currentJump = 0;

                if (pounding)
                    pounding = false;

                FollowX(boxCast.collider.transform);
                Stop(0);
            }
            return true;
        }
        return false;
	}
    public void InvertGravity()
    {
        transform.DOScaleY(-1*transform.localScale.y, 0.2f);
        gravityBias = -gravityBias;
    }
}
