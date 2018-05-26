using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transitor : MonoBehaviour {

	private void Start(){
		Transition(0,0,0);
	}
	public void Transition(float init_scale, float final_scale, float duration){
		transform.DOScale(new Vector3(1,1,1)*init_scale, 0f);
		transform.DOScale(new Vector3(1,1,1)*final_scale, duration);
	}
}
