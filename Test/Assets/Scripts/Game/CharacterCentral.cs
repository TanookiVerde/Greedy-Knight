using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterCentral: MonoBehaviour {
	[Header("Game Over")]
	[SerializeField] private int bloodCells;
	[SerializeField] private GameObject bloodPrefab;
	[SerializeField] private GameOverPanel gameOverPanel;

	private CharacterMovement characterMovement;
	private Rigidbody2D rb;

	private void Start(){
		rb = GetComponent<Rigidbody2D>();
		characterMovement = GetComponent<CharacterMovement>();
	}
	private void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Obstacles"){
			Die();
		}else if(other.gameObject.tag == "Goods"){
			//GET GOOD
		}
	}
	private void Die(){
		for(int i = 0; i < bloodCells; i++){
			var b = Instantiate(bloodPrefab,transform.position,Quaternion.identity);
			b.GetComponent<Rigidbody2D>().AddForce(Vector2.up*Random.Range(1f,2f) + Vector2.right*Random.Range(-1f,1f)*2f);
			b.transform.DOScale(0f,1f);
		}
		int deathCount = PlayerPrefs.GetInt("deathCount",0);
		PlayerPrefs.SetInt("deathCount",++deathCount);
		characterMovement.NotAlive();
		gameOverPanel.ShowGameOverPanel();
		Destroy(this.gameObject);
	}
}
