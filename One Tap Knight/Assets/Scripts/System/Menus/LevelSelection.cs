using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelection : MonoBehaviour {
	private const int LEVEL_QUANTITY = 9;
	private const int STARS_PER_LEVEL = 3;
	private const float CANVAS_SIZE = 1280;

	[SerializeField] private float levelInfoSpeed;
	[SerializeField] private Level[] levelData = new Level[LEVEL_QUANTITY];
	[SerializeField] private RectTransform levelHUD;
	[SerializeField] private LevelDots dots;

	private int[,] levelProgression = new int[LEVEL_QUANTITY,STARS_PER_LEVEL];
	private int currentLevel = 0;

	public void Start()
	{
		InitializeLevelInfo();
		dots.ChangeSelectedLevel(currentLevel);
	}
	private void InitializeLevelInfo()
	{
		levelHUD.GetComponent<LevelInfo>().ChangeInfo(
			levelData[currentLevel].title,
			levelData[currentLevel].img,
			CreateStarArray(currentLevel));
	}
	public void ChangeLevel(int changeDirection)
	{
		if(!IsLevelInBounds(changeDirection)) 
			return;
		StartCoroutine(ChangeLevelAnimation(changeDirection));
	}
	private IEnumerator ChangeLevelAnimation(int changeDirection)
	{
		levelHUD.DOAnchorPosX(-changeDirection*CANVAS_SIZE,levelInfoSpeed);
		yield return new WaitForSeconds(levelInfoSpeed);
		currentLevel += changeDirection;
		dots.ChangeSelectedLevel(currentLevel);
		InitializeLevelInfo();
		levelHUD.DOAnchorPosX(changeDirection*CANVAS_SIZE, 0.00000001f);
		levelHUD.DOAnchorPosX(0,levelInfoSpeed);
	}
	private bool IsLevelInBounds(int changeDirection)
	{
		return currentLevel + changeDirection < LEVEL_QUANTITY && currentLevel + changeDirection >= 0;
	}
	private int[] CreateStarArray(int currentLevel)
	{
		int[] ret = {levelProgression[currentLevel,0],
			levelProgression[currentLevel,1],
			levelProgression[currentLevel,2]};
		return ret;
	}
}
