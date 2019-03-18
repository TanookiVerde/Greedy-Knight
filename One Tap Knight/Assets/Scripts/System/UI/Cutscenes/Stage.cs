using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Stage : MonoBehaviour {

    [SerializeField] private List<Story> stories;

    [SerializeField] private Image slideImage;
    [SerializeField] private TMP_Text slideText;

    public void Start()
    {
        int index = PlayerPrefs.GetInt("cutscene",0);
        StartCoroutine(PlayStory(stories[index]));
    }
    private IEnumerator PlayStory(Story story)
    {
        PlayerPrefs.SetInt("cutscene", 0);
        slideImage.DOFade(0, 0);
        slideText.DOFade(0, 0);
        slideText.text = "";
        yield return new WaitForSeconds(2f);
        slideText.DOFade(1, 0.5f);
        for (int i = 0; i < story.scenes.Count; i++)
        {
            slideImage.DOFade(1, 0.5f);
            slideImage.sprite = story.scenes[i].image;
            for (int j = 0; j < story.scenes[i].texts.Count; j++)
            {
                slideText.DOFade(1, 0.5f);
                yield return new WaitForSeconds(1f);
                yield return ShowText(story.scenes[i].texts[j]);
                yield return new WaitForSeconds(2f);
                slideText.DOFade(0, 0.5f);
                yield return new WaitForSeconds(0.5f);
                slideText.text = "";
            }
            slideImage.DOFade(0, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        slideText.DOFade(0, 0.5f);
        Transition.transition.TransiteTo("MainMenu");
    }
    private IEnumerator ShowText(string text)
    {
        for(int i = 0; i <= text.Length; i++)
        {
            slideText.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.025f);
        }
    }
}
