using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuPanel : MonoBehaviour {
    private const float FINAL_POSITION = 0;
    private const float INITIAL_POSITION = -720;

	public virtual void Open()
    {
        GetComponent<RectTransform>().DOAnchorPosY(FINAL_POSITION, 0.25f);
    }
    public virtual void BackToMenu()
    {
        GetComponent<RectTransform>().DOAnchorPosY(INITIAL_POSITION, 0.25f);
    }
}
