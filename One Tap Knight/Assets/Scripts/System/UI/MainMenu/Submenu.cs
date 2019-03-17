using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Submenu : MonoBehaviour {
    [SerializeField] private List<RectTransform> menuObjects;
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private CanvasGroup group;

	public void Open()
    {
        group.interactable = true;
        group.blocksRaycasts = true;
        group.alpha = 1;
        if(firstSelected != null)
            firstSelected.Select();
        OnOpen();
    }
    public void Close()
    {
        if (firstSelected != null)
            firstSelected.Select();
        group.interactable = false;
        group.blocksRaycasts = false;
        group.alpha = 0;
        OnClose();
    }
    public void FastClose()
    {
        if (firstSelected != null)
            firstSelected.Select();
        group.interactable = false;
        group.blocksRaycasts = false;
        group.alpha = 0;
    }
    protected virtual void OnOpen() { }
    protected virtual void OnClose() { }
}
