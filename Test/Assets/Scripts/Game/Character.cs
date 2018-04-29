using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour {

	private Rigidbody2D rb;
	private CharacterMovement characterMovement;
	public GameOverPanel gameOverPanel;

	private void Start(){
		rb = GetComponent<Rigidbody2D>();
		characterMovement = GetComponent<CharacterMovement>();
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.tag == "Obstacles"){
			DieAnimation();
			characterMovement.NotAlive();
			gameOverPanel.RotateGameOverPanel(0,0.5f);
		}else if(other.gameObject.tag == "Goods"){
			//GET GOOD
		}
	}
	private void DieAnimation(){
		transform.DORotate(new Vector3(0,0,90), 0.2f);
		transform.DOMoveY(transform.position.y - 1f, 0.2f);
		GetComponent<Animator>().enabled = false;
		GetComponent<SpriteRenderer>().DOColor(Color.grey,0.2f);
	}
}
