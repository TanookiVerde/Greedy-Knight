using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour {

	public UnityEvent onTrigger;

	void OnTriggerEnter2D(Collider2D other)
	{
		Trigger();
	}
	public void Trigger()
	{
		GetComponent<Collider2D>().enabled = false;
		onTrigger.Invoke();
	}
}
