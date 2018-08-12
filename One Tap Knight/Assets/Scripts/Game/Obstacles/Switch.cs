using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer), typeof(Collider2D))]
public class Switch : MonoBehaviour {

	[SerializeField]
	private Sprite upSprite;
	[SerializeField]
	private Sprite downSprite;

	[SerializeField]
	private UnityEvent onActivate;

	void Start()
	{
		GetComponent<SpriteRenderer>().sprite = upSprite;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(other.gameObject.GetComponent<Character>().IsPounding())
			{
				Activate();
			}
		}
	}
	void Activate()
	{
		GetComponent<SpriteRenderer>().sprite = downSprite;
	 	onActivate.Invoke();
	}
}
