using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour {
	private void Start(){
		StartCoroutine(MenuAnimationStructure());
	}
	private void Animate(int rand){
		int r = Random.Range(0,2);
		if(r == 0) a_ScaleX(rand);
		if(r == 1) a_ScaleY(rand);
	}
	private void a_ScaleY(int rand){
		Sequence s = DOTween.Sequence();
		s.Append(transform.GetChild(rand).DOScaleY(0,0.4f));
		s.Append(transform.GetChild(rand).GetComponent<Image>().DOColor(GetRandomColor(),0));
		s.Append(transform.GetChild(rand).DOScaleY(1,0.4f));
		s.Play();
	}
	private void a_ScaleX(int rand){
		Sequence s = DOTween.Sequence();
		s.Append(transform.GetChild(rand).DOScaleX(0,0.4f));
		s.Append(transform.GetChild(rand).GetComponent<Image>().DOColor(GetRandomColor(),0));
		s.Append(transform.GetChild(rand).DOScaleX(1,0.4f));
		s.Play();
	}
	private IEnumerator MenuAnimationStructure(){
		yield return FirstAnimation();
		yield return AnimationLoop();
	}
	private IEnumerator FirstAnimation(){
		for(int i = 0; i<transform.childCount;i++){
			Animate(i);
			yield return new WaitForSeconds(0.01f);
		}
	}
	private IEnumerator AnimationLoop(){
		while(true){
			Animate(Random.Range(0,transform.childCount));
			yield return new WaitForSeconds(0.1f);
		}
	}
	private Color GetRandomColor(){
		return new Color(Random.Range(0,1f),Random.Range(0,1f),Random.Range(0,1f),1f);
	}
}
