using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Stalactite : MonoBehaviour {

	public float dropPerFrame = 0.01f;
    public int frameDuration = 50;
    public float timeBetweenFrames = 0.05f;

	[Header("Grounded")]
    [SerializeField] private Transform groundPosition;
    [SerializeField] private Vector2 groundBoxCastSize;
    
	public void StartActions()
	{
		StartCoroutine(Shake());		
	}
	IEnumerator Shake()
	{
		Vector3 startPosition = transform.position;
		Vector2 circlePosition;
		for(int i = 0; i < frameDuration; i ++)
		{
			circlePosition = Random.insideUnitCircle/20;
			transform.position = startPosition + (Vector3)circlePosition;
			yield return new WaitForSeconds(timeBetweenFrames);
		}
		StartCoroutine(Fall());
	}
	IEnumerator Fall()
	{
		while(!IsGrounded())
		{
			transform.position -= new Vector3(0, dropPerFrame);
			yield return new WaitForFixedUpdate();
		}
		gameObject.layer = LayerMask.NameToLayer("Ground");
		gameObject.tag = "Untagged";
		this.enabled = false;
	}
	private bool IsGrounded()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(groundPosition.position,
                groundBoxCastSize, 0, Vector2.up, 0, LayerMask.GetMask("Ground", "Stop"));

        if (boxCast.collider != null)
        {
			return true;
        }
        return false;
    }
}
