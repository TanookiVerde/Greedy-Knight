using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EndPanel : MonoBehaviour {
    private const float FINAL_POSITION = 30;

    public float timeToAppear;
    public CanvasGroup darkBackground;
    public CanvasGroup infoPanel;

    public TMP_Text deathCount;
    public TMP_Text diamondCount;

    public RectTransform confirmButton;
    public RectTransform replayButton;

    public void Show()
    {
        darkBackground.DOFade(1, timeToAppear);
        infoPanel.DOFade(0, 0);
        GetComponent<RectTransform>().DOAnchorPosY(FINAL_POSITION, timeToAppear);
        StartCoroutine(ShowInfoPanel());
    }
    private IEnumerator ShowInfoPanel()
    {
        diamondCount.transform.parent.DOScale(0, 0);
        deathCount.transform.parent.DOScale(0, 0);
        confirmButton.DOAnchorPosX(confirmButton.anchoredPosition.x + 720, 0);
        replayButton.DOAnchorPosX(replayButton.anchoredPosition.x + 720, 0);

        yield return new WaitForSeconds(0.5f);
        infoPanel.DOFade(1, timeToAppear);
        yield return new WaitForSeconds(0.5f);
        deathCount.text = Random.Range(0, 10).ToString();
        deathCount.transform.parent.DOScale(1, 0.1f);
        yield return new WaitForSeconds(0.5f);
        diamondCount.text = Random.Range(0, 10).ToString();
        diamondCount.transform.parent.DOScale(1, 0.1f);
        yield return new WaitForSeconds(0.5f);
        replayButton.DOAnchorPosX(replayButton.anchoredPosition.x - 720, 0.2f);
        yield return new WaitForSeconds(0.25f);
        confirmButton.DOAnchorPosX(confirmButton.anchoredPosition.x - 720, 0.2f);
    }
}
