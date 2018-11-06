using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour {
    private const float SCREEN_WIDTH = 1280;
    private const float SCREEN_HEIGHT = 720;

    public RectTransform background;
    public RectTransform logo;
    public RectTransform knight;
    public TMP_Text touchToStart;

    public List<Image> slides;

    private int currentSlide;

    private void Start()
    {
        StartCoroutine(MenuAnimation());
        HideAllSlides();
        ShowSlide(currentSlide);
    }
    private IEnumerator MenuAnimation()
    {
        float logo_y = logo.anchoredPosition.y;
        float knight_x = knight.anchoredPosition.x;
        float bg_x = background.anchoredPosition.x;
        logo.DOAnchorPosY(logo_y + SCREEN_HEIGHT, 0f);
        knight.DOAnchorPosX(knight_x - SCREEN_WIDTH, 0f);
        background.DOAnchorPosX(bg_x - SCREEN_WIDTH, 0f);
        background.DOAnchorPosX(bg_x, 0.5f);
        yield return new WaitForSeconds(0.5f);
        logo.DOAnchorPosY(logo_y, 0.5f);
        yield return new WaitForSeconds(0.5f);
        knight.DOAnchorPosX(knight_x, 0.5f);
        yield return new WaitForSeconds(0.5f);
        yield return MenuLoop();
    }
    private IEnumerator MenuLoop()
    {
        while (true)
        {
            slides[currentSlide].DOFade(0, 0.5f);
            currentSlide = (currentSlide + 1) % slides.Count;
            slides[currentSlide].DOFade(1, 0.5f);
            slides[currentSlide].transform.DOScale(1f, 0f);
            slides[currentSlide].transform.DOScale(1.1f, 4f);
            yield return new WaitForSeconds(3f);
        }
    }
    private void HideAllSlides()
    {
        foreach(var s in slides)
        {
            s.DOFade(0, 0);
        }
    }
    private void ShowSlide(int i)
    {
        slides[i].DOFade(1, 0);
    }
}
