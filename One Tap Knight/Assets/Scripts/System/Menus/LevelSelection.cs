using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelection : MonoBehaviour, IScreen {
	private const int LEVEL_QUANTITY = 9;
	private const int STARS_PER_LEVEL = 3;
	private const float CANVAS_SIZE = 1280;

	[SerializeField] private float levelInfoSpeed;
	[SerializeField] private Level[] levelData = new Level[LEVEL_QUANTITY];
	[SerializeField] private RectTransform levelHUD;
	[SerializeField] private LevelDots dots;

	private int[,] levelProgression = new int[LEVEL_QUANTITY,STARS_PER_LEVEL];
	private int currentLevel = 0;

	public void Prepare()
	{
		levelProgression = SaveAndLoad.LoadLevelData();
		if(SaveAndLoad.GetFinishedLevel()){
			SaveAndLoad.SetFinishedLevel(false);
			StartCoroutine(UnlockLevelAnimation());
		}else{
			currentLevel = 0;
			SetLevelInfo();
			dots.ChangeSelectedLevel(currentLevel);
		}
	}
	public void Close()
    {
        //
    }
	private void SetLevelInfo()
	{
		levelHUD.GetComponent<LevelInfo>().ChangeInfo(
			levelData[currentLevel],
			CreateStarArray(currentLevel),
			IsCompletedOrUnlocked(currentLevel),
			currentLevel != 0,
			currentLevel != LEVEL_QUANTITY-1
			);
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
		SetLevelInfo();
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
	private int LastCompletedLevel()
	{
		for(int level = 0; level < LEVEL_QUANTITY; level++)
		{
			if(levelProgression[level,0] == 0)
			{
				return level;
			}
		}
		return -1;
	}
	private void CompleteLevels(int amount)
	{
		for(int i = 0; i < amount; i++)
		{
			levelProgression[i,0] = 1;
		}
	}
	private bool IsCompletedOrUnlocked(int level)
	{
		return levelProgression[level,0] == 1 || level == 0 || levelProgression[level-1,0] == 1;
	}
	private IEnumerator UnlockLevelAnimation()
	{
		currentLevel = LastCompletedLevel();
		SetLevelInfo();
		dots.ChangeSelectedLevel(currentLevel);
		yield return levelHUD.GetComponent<LevelInfo>().Unlock();
		yield return new WaitForEndOfFrame();
	}
}
