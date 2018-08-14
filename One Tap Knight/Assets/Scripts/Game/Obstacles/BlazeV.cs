using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeV : MonoBehaviour {

	public float velPerFrame = 0.2f;
	public float accelPerFrame = 0.01f;

	private float temp = 0;
	private float startY;

	void Start () 
	{
		startY = transform.position.y;
	}
	public void StartActions()
	{
		StartCoroutine(Jump());		
	}
	IEnumerator Jump()
	{
		temp = velPerFrame;
		while(temp >= 0)
		{
			transform.position += new Vector3(0, temp);
			temp -= accelPerFrame;
            yield return new WaitForFixedUpdate();
		}
		if(temp < 0)
			temp = 0;

		GetComponent<SpriteRenderer>().flipY = true;
		StartCoroutine(Fall());
	}
	IEnumerator Fall()
	{
		while(transform.position.y > startY)
		{
			transform.position -= new Vector3(0, temp);
			temp += accelPerFrame;
			yield return new WaitForFixedUpdate();
        }
		transform.position = new Vector3(transform.position.x, startY, transform.position.z);
		GetComponent<SpriteRenderer>().flipY = false;
	}
}
