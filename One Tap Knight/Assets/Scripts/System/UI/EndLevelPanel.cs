using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndLevelPanel : MonoBehaviour {

    [SerializeField] private Image background;
    [SerializeField] private CanvasGroup elements;
    [SerializeField] private Text deathsStats;
    [SerializeField] private Text coinsStats;
    [SerializeField] private float duration;

    private void Start()
    {
        SetActiveInstant(false);
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            SetInformation();
            background.DOFillAmount(1, duration);
            elements.DOFade(1, duration);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            background.DOFillAmount(0, duration);
            elements.DOFade(0, duration);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    public void SetInformation()
    {
        deathsStats.text = PlayerPrefs.GetInt("deathCount", 0).ToString();
        coinsStats.text = LevelManager.collectedCoins + "/" + Coin.totalCoin;
    }
    private void SetActiveInstant(bool active)
    {
        if (active)
        {
            SetInformation();
            background.DOFillAmount(1, 0);
            elements.DOFade(1, 0);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            background.DOFillAmount(0, 0);
            elements.DOFade(0, 0);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
