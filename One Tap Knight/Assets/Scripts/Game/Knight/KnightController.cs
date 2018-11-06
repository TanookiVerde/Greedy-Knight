using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KnightController : MonoBehaviour {
    public GameObject deathSpawn;

    [Header("Preferences")]
    public float movingVelocity;
    public float jumpIntensity;
    public int jumpLimit;
    public float groundPoundStopTime;

    [Header("Ground Check")]
    public Transform ground;
    public float distanceToGround;

    private new Rigidbody2D rigidbody2D;
    private float currentTax = 1;
    private int jumpsRemaining;

    public bool isPounding;
    public bool finishedLevel;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void MovementLoop()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.W))
            Jump();
        if (Input.GetKeyDown(KeyCode.S) && !isPounding)
            Pound();
    }
    private void Move()
    {
        if(currentTax != 0 && !isPounding)
        {
            rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal") * movingVelocity * currentTax, rigidbody2D.velocity.y);
        }
    }
    private void Jump()
    {
        if (IsGrounded() || jumpsRemaining > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            rigidbody2D.AddForce(Vector2.up * jumpIntensity);
            jumpsRemaining--;
        }
    }
    private void Pound()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isPounding)
        {
            StartCoroutine(PoundCoroutine());
        }
    }
    private IEnumerator PoundCoroutine()
    {
        isPounding = true;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.AddForce(Vector2.up * jumpIntensity * -1);
        while (!IsGrounded())
            yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(groundPoundStopTime);
        isPounding = false;
    }
    public void Die()
    {
        Instantiate(deathSpawn, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    public void GetDiamond()
    {
        print("PLAYER GOT A DIAMOND");
    }
    public void ModifyVelocity(float tax)
    {
        currentTax = tax;
    }
    public bool IsGrounded()
    {
        var ray = Physics2D.Raycast(ground.position, Vector2.down, distanceToGround, 1 << LayerMask.NameToLayer("Ground"));
        bool b = ray.collider != null;
        if(b) jumpsRemaining = jumpLimit;
        return b;
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("End"))
        {
            finishedLevel = true;
            print("FINISHED");
        }
    }
}
