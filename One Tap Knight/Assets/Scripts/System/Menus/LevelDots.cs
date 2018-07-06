using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelDots : MonoBehaviour {

	[SerializeField] float maxScale;
	[SerializeField] float scaleChangeDuration;

	private int lastSelected = 0;

	public void ChangeSelectedLevel(int current)
	{
		if(lastSelected != current){
			transform.GetChild(lastSelected).transform.DOScale(1,scaleChangeDuration*2);
			transform.GetChild(current).transform.DOScale(maxScale,scaleChangeDuration);
			lastSelected = current;
		}else{
			transform.GetChild(current).transform.DOScale(maxScale,scaleChangeDuration);
		}
	}
}
