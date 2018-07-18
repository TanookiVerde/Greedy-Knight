using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingPanel : MonoBehaviour {

    [SerializeField] private CanvasGroup group;
    [SerializeField] private Image loadingIcon;

    public void Appear()
    {
        group.DOFade(1, 0.5f);
        group.blocksRaycasts = true;
    }
    public void Disappear()
    {
        group.DOFade(0, 0);
        group.blocksRaycasts = false;
    }
}
