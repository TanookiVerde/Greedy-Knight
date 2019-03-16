using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkipCutscene : MonoBehaviour {
    [SerializeField] private float hold;
    [SerializeField] private float taxByFrame;
    [SerializeField] private Image fill;
    [SerializeField] private RectTransform rect;

    private bool skipped;

    private void Update()
    {
        if (!skipped)
        {
            if (Input.GetKey(KeyCode.Escape))
                hold += taxByFrame;
            else
                hold -= taxByFrame;
            hold = Mathf.Clamp(hold, 0, 100);
            fill.fillAmount = hold / 100f;
            if (hold == 100)
            {
                Transition.transition.TransiteTo("MainMenu");
                skipped = true;
                rect.DOAnchorPosY(rect.anchoredPosition.y + 100f, 0.5f);
            }
        }
    }
}
