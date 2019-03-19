using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EndPanel : MonoBehaviour {
    private const float FINAL_POSITION = -330;

    public float timeToAppear;
    public CanvasGroup infoPanel;
    
    public TMP_Text diamondCount;
    public List<RectTransform> buttons;

    public void Show()
    {
        infoPanel.interactable = true;
        GetComponent<RectTransform>().DOAnchorPosX(FINAL_POSITION, timeToAppear);
        StartCoroutine(ShowInfoPanel());
        buttons[0].GetComponent<Selectable>().Select();
    }
    private IEnumerator ShowInfoPanel()
    {
        diamondCount.transform.parent.DOScale(0, 0);
        buttons[0].DOAnchorPosX(buttons[0].anchoredPosition.x + 720, 0);
        buttons[1].DOAnchorPosX(buttons[1].anchoredPosition.x + 720, 0);

        yield return new WaitForSeconds(0.5f);
        infoPanel.DOFade(1, timeToAppear);
        yield return new WaitForSeconds(0.5f);
        diamondCount.text = Diamond.collectedDiamonds + "/" + Diamond.totalDiamonds;
        diamondCount.transform.parent.DOScale(1, 0.1f);
        yield return new WaitForSeconds(0.25f);
        buttons[0].DOAnchorPosX(buttons[0].anchoredPosition.x - 720, 0.2f);
        yield return new WaitForSeconds(0.25f);
        buttons[1].DOAnchorPosX(buttons[1].anchoredPosition.x - 720, 0.2f);
    }
    public void BackToMenu()
    {
        Camera.main.GetComponent<AudioSource>().DOFade(0, 0.25f);
        Destroy(FindObjectOfType<MusicManager>().gameObject, 0.25f);
        Transition.transition.TransiteTo("MainMenu");
    }
    public void Reset()
    {
        Transition.transition.TransiteTo(PlayerPrefs.GetString("levelName"));
    }
}
