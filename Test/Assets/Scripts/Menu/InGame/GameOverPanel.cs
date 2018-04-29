using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

	private void Start(){
		RotateGameOverPanel(90,0f);
	}
	public void RotateGameOverPanel(float angle, float duration){
		transform.DORotate(new Vector3(0,0,angle), duration);
	}
	public void RestartLevel(){
		SceneManager.LoadScene("MainScene");
	}
}
