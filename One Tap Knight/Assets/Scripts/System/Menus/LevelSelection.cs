using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelection : MonoBehaviour, IScreen {
	private const int LEVEL_QUANTITY = 9;
	private const int STARS_PER_LEVEL = 3;
	private const float CANVAS_SIZE = 1280;

    private const float MAX_SCALE = 1.1f;
    private const float ANIM_DURATION = 0.5f;
    private const float OBJ_DELAY = 0.25f;
    private const float INITIAL_DELAY = 0.4f;

    [SerializeField] private List<GameObject> objectsToAnimate;
    [SerializeField] private float levelInfoSpeed;
	[SerializeField] private Level[] levelData = new Level[LEVEL_QUANTITY];
	[SerializeField] private RectTransform levelHUD;
	[SerializeField] private LevelDots dots;

	[SerializeField] private SaveData save;
	private int currentLevel = 0;

	public void Prepare()
	{
        save = SaveAndLoad.LoadLevelData();
		if(SaveAndLoad.GetFinishedLevel()){
			SaveAndLoad.SetFinishedLevel(false);
			StartCoroutine(UnlockLevelAnimation());
		}else{
			currentLevel = 0;
			SetLevelInfo();
			dots.ChangeSelectedLevel(currentLevel);
		}
        SaveAndLoad.SetLastOpenedLevel(currentLevel);
        SaveAndLoad.SetLastOpenedLevelName(levelData[currentLevel].sceneName);
    }

    public void Close()
    {
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
        print(levelData[currentLevel].sceneName);
	}

	private IEnumerator ChangeLevelAnimation(int changeDirection)
	{
		levelHUD.DOAnchorPosX(-changeDirection*CANVAS_SIZE,levelInfoSpeed);
		yield return new WaitForSeconds(levelInfoSpeed);
		currentLevel += changeDirection;
        SaveAndLoad.SetLastOpenedLevel(currentLevel);
        SaveAndLoad.SetLastOpenedLevelName(levelData[currentLevel].sceneName);

        dots.ChangeSelectedLevel(currentLevel);
		SetLevelInfo();
		levelHUD.DOAnchorPosX(changeDirection*CANVAS_SIZE, 0.00000001f);
		levelHUD.DOAnchorPosX(0,levelInfoSpeed);

        yield return new WaitForSeconds(INITIAL_DELAY);
        yield return BeginningAnimation();

    }

	private bool IsLevelInBounds(int changeDirection)
	{
		return currentLevel + changeDirection < LEVEL_QUANTITY && currentLevel + changeDirection >= 0;
	}

	private int[] CreateStarArray(int currentLevel)
	{
		int[] ret = { save.levelCompleted[currentLevel], save.allCoins[currentLevel], save.noCoins[currentLevel] };
		return ret;
	}

	private int LastCompletedLevel()
	{
		for(int level = 0; level < LEVEL_QUANTITY; level++)
		{
			if(save.levelCompleted[level] == 0)
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
            save.levelCompleted[i] = 1;
		}
	}

	private bool IsCompletedOrUnlocked(int level)
	{
		return save.levelCompleted[level] == 1 || level == 0 || save.levelCompleted[level-1] == 1;
	}

	private IEnumerator UnlockLevelAnimation()
	{
		currentLevel = LastCompletedLevel();
        SetLevelInfo();
		dots.ChangeSelectedLevel(currentLevel);
		yield return levelHUD.GetComponent<LevelInfo>().Unlock();
		yield return new WaitForFixedUpdate();
    }

    public IEnumerator BeginningAnimation()
    {
        foreach (GameObject go in objectsToAnimate)
        {
            Sequence s = DOTween.Sequence();
            s.Append(go.transform.DOScale(MAX_SCALE, ANIM_DURATION / 2));
            s.Append(go.transform.DOScale(1, ANIM_DURATION / 2));
            yield return new WaitForSeconds(OBJ_DELAY);
        }
    }
}
