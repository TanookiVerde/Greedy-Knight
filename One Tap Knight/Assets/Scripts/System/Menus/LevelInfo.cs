using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {

	[SerializeField] private Text levelName;
	[SerializeField] private Image levelImg;
	[SerializeField] private List<Image> levelStars;
	[SerializeField] private Image locked;
	[SerializeField] private GameObject playButton;
	[SerializeField] private GameObject prevButton;
	[SerializeField] private GameObject nextButton;

	private Level lastLevel;

	public void ChangeInfo(Level level, int[] stars, bool unlocked, bool prev, bool next)
	{
		lastLevel = level;

		playButton.SetActive(unlocked);
        playButton.transform.DOScale(1, 0.5f);

        prevButton.SetActive(prev);
		nextButton.SetActive(next);
		levelName.text = level.title;
		levelImg.sprite = level.img;
		locked.DOFade(unlocked ? 0 : 1,0);
		for(int i = 0; i < 3; i++)
		{
			levelStars[i].gameObject.SetActive(stars[i] > 0);
		}
	}
	public IEnumerator Unlock()
	{
		locked.DOFade(1,0);
		yield return new WaitForSeconds(0.5f);
		locked.DOFade(0,0.5f);
		yield return new WaitForSeconds(0.5f);
		playButton.SetActive(true);
	}
	public void LoadLevel()
	{
        StartCoroutine(LoadLevelAnimation());
	}
    private IEnumerator LoadLevelAnimation()
    {
        yield return null;
        var transition = GameObject.Find("_LOADING_SCREEN").GetComponent<LoadingPanel>();
        transition.Appear();
        SceneManager.LoadSceneAsync(lastLevel.sceneName);
    }
}
