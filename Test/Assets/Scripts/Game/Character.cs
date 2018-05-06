using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour {
	[SerializeField] private GameObject bloodPrefab;
	[SerializeField] private GameOverPanel gameOverPanel;

	private CharacterMovement characterMovement;
	private Rigidbody2D rb;

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
		for(int i = 0; i < 10; i++){
			var b = Instantiate(bloodPrefab,transform.position,Quaternion.identity);
			b.GetComponent<Rigidbody2D>().AddForce(Vector2.up*Random.Range(1f,2f) + Vector2.right*Random.Range(-1f,1f)*2f);
			b.transform.DOScale(0f,1f);
		}
		Destroy(this.gameObject);
	}
}
