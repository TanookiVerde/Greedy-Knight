using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeH : MonoBehaviour {
	
[Header("Movement Parameters")]
    public float speed;
    public float size;
    private int bias;

[Header("Animation Parameters")]
	public GameObject sparksParticles;

    private Rigidbody2D myRB;
    private SpriteRenderer mySR;
    
	void Start ()
    {
        myRB = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();

        bias = -1;
        myRB.velocity = new Vector2(speed*bias, 0);
	}
    private void FixedUpdate()
    {
        if(HasWall())
        {
			Explode();
        }
    }
    private bool HasWall()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D ray = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            size*bias,
            1 << LayerMask.NameToLayer("Ground")
            );
        Physics2D.queriesStartInColliders = true;
		return ray.collider != null;
    }
    private void OnDrawGizmos()
    {
        //HasWall Ray
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * bias * size);
        //Collider
        Vector3 center = transform.position + (Vector3) GetComponent<CircleCollider2D>().offset * transform.localScale.x;
        Gizmos.DrawWireSphere(center, GetComponent<CircleCollider2D>().radius*transform.localScale.x);
    }
	private void Explode()
	{
		sparksParticles.transform.position = transform.position;
		GameObject g = Instantiate(sparksParticles);
		Destroy(g, 3f);
		Destroy(gameObject);
	}
}
