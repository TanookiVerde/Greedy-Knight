using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {

    [SerializeField] private CanvasGroup elements;
    [SerializeField] private Text deathsStats;
    [SerializeField] private float duration;

    private void Start()
    {
        SetActiveInstant(false);
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            GetInformation();
            elements.DOFade(1, duration);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            elements.DOFade(0, duration);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    private void GetInformation()
    {
        deathsStats.text = PlayerPrefs.GetInt("deathCount", 0).ToString();
    }
    private void SetActiveInstant(bool active)
    {
        if (active)
        {
            GetInformation();
            elements.DOFade(1, duration);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            elements.DOFade(0, duration);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync("Level 1"); //mudar para o level correto
    }
}
