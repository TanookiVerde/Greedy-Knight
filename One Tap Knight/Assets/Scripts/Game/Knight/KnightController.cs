using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightController : MonoBehaviour
{
    public GameObject deathSpawn;
    public KnightSound sound;

    [Header("Preferences")]
    public float movingVelocity;
    public float jumpIntensity;
    public int jumpLimit;
    public float groundPoundStopTime;
    public float lookDownMinTime;

    [Header("Ground Check")]
    public Transform ground;
    public float distanceToGround;
    public Vector2 groundBoxCastSize;

    private Transform followTransform;
    private float followXOffset;
    private bool following = false;

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private float currentTax = 1;
    [HideInInspector]
    public int jumpsRemaining;

    public bool isPounding;
    public bool finishedLevel;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ground.position, groundBoxCastSize);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        jumpsRemaining = jumpLimit;
        rigidbody2D = GetComponent<Rigidbody2D>();
        sound = GetComponent<KnightSound>();
        Time.timeScale = 1f;
    }
    public void MovementLoop()
    {
        Move();
        animator.SetFloat("XVelocity", rigidbody2D.velocity.x);
        bool grounded = IsGrounded();
        if (grounded || jumpsRemaining > 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        if (!grounded && Input.GetButtonDown("Pound") && !isPounding)
        {
            StartCoroutine(PoundCoroutine());
            StartCoroutine(LookDownCoroutine());
        }
    }
    private void Move()
    {
        if (!isPounding && !following)
        {
            rigidbody2D.velocity = new Vector2(movingVelocity * currentTax, rigidbody2D.velocity.y);
        }
        else if (following)
        {
            transform.position = new Vector3(followTransform.position.x - followXOffset, transform.position.y, transform.position.z);
        }
    }
    public void FollowX(Transform t)
    {
        followTransform = t;
        followXOffset = followTransform.position.x - transform.position.x;
        rigidbody2D.velocity = Vector2.zero;
        following = true;
    }
    public void StopFollowing()
    {
        followTransform = null;
        followXOffset = 0;
        following = false;
    }
    private void Jump()
    {
        sound.PlaySound(SoundType.JUMP);
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
        rigidbody2D.AddForce(Vector2.up * jumpIntensity);
        GetComponent<Animator>().Play("jump");
        jumpsRemaining--;
    }
    private void Pound()
    {
        StartCoroutine(PoundCoroutine());
    }
    private IEnumerator PoundCoroutine()
    {
        isPounding = true;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(Vector2.up * jumpIntensity * -1);
        while (!IsGrounded())
            yield return new WaitForEndOfFrame();
        sound.PlaySound(SoundType.FALL);
        yield return new WaitForSeconds(groundPoundStopTime);
        isPounding = false;
    }
    private IEnumerator LookDownCoroutine()
    {
        float time = 0;
        while (time < lookDownMinTime)
        {
            if (!Input.GetKey(KeyCode.S))
                yield break;
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.GetComponent<CameraMovement>().LookDown();
        while (Input.GetKey(KeyCode.S))
        {
            yield return null;
        }
        Camera.main.GetComponent<CameraMovement>().ResetLook();
    }
    public void Die(bool isSpike = false)
    {
        if (isSpike && rigidbody2D.velocity.y > 2f)
            return;
        sound.PlaySound(SoundType.DIE);
        Instantiate(deathSpawn, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    public void ModifyVelocity(float tax)
    {
        currentTax = tax;
    }
    private bool IsGrounded()
    {
        if (rigidbody2D.velocity.y > 0) return false;
        RaycastHit2D boxCast = Physics2D.BoxCast(ground.position,
                groundBoxCastSize, 0, Vector2.up, 0, LayerMask.GetMask("Ground", "Stop"));

        if (boxCast.collider != null)
        {
            jumpsRemaining = jumpLimit;
            return true;
        }
        return false;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("End"))
        {
            finishedLevel = true;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}


