using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelInfo : MonoBehaviour {

	[SerializeField] private Text levelName;
	[SerializeField] private Image levelImg;
	[SerializeField] private List<Image> levelStars;

	public void ChangeInfo(string name, Sprite img, int[] stars)
	{
		levelName.text = name;
		levelImg.sprite = img;
		for(int i = 0; i < 3; i++)
		{
			levelStars[i].gameObject.SetActive(stars[i] > 0);
		}
	}
}
