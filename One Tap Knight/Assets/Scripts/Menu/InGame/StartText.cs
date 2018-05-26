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
		GetComponent<Text>().DOFade(0,0);
		anim = StartCoroutine(UpAndDownLoop());
	}
	private IEnumerator UpAndDownLoop(){
		while(!CharacterMovement.canStart){yield return new WaitForEndOfFrame();}
		GetComponent<Text>().DOFade(1,0.5f);
		while(true){
			GetComponent<RectTransform>().DOAnchorPosY(current_y + amplitude, duration);
			yield return new WaitForSeconds(duration/2);
			GetComponent<RectTransform>().DOAnchorPosY(current_y - amplitude, duration);
			yield return new WaitForSeconds(duration/2);
		}
	}
	public void StopAnimation(){
		StopCoroutine(anim);
		GetComponent<Text>().DOFade(0, 0.25f);
	}
}
