using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartText : MonoBehaviour {

	public float duration;
	public float amplitude;

	private float current_y;
	private Coroutine anim;

	private void Start(){
		current_y = GetComponent<RectTransform>().anchoredPosition.y;
		StartAnimation();
	}
	private IEnumerator UpAndDownLoop(){
		while(true){
			GetComponent<RectTransform>().DOAnchorPosY(current_y + amplitude, duration);
			yield return new WaitForSeconds(duration/2);
			GetComponent<RectTransform>().DOAnchorPosY(current_y - amplitude, duration);
			yield return new WaitForSeconds(duration/2);
		}
	}
	public void StartAnimation(){
		anim = StartCoroutine(UpAndDownLoop());
	}
	public void StopAnimation(){
		StopCoroutine(anim);
		GetComponent<Text>().DOFade(0, 0.25f);
	}
}
