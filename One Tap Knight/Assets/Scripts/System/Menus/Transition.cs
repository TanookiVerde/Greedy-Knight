using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Transition : MonoBehaviour {
    [SerializeField] private GameObject transitionImg;
    [SerializeField] private LoadingPanel loadingPanel;

    public float TRANSITION_DURATION = 0.5f;
    private const float MAX_SCALE = 35; 

	public void ToggleTransition(bool hasFirstHalf)
    {
        StartCoroutine(Transite(hasFirstHalf));
    }
    public IEnumerator Transite(bool firstHalf,bool loading = false)
    {
        if (firstHalf)
        {
            transitionImg.transform.DOScale(0, 0);
            transitionImg.transform.DOScale(MAX_SCALE, TRANSITION_DURATION);
        }
        else
        {
            transitionImg.transform.DOScale(MAX_SCALE, 0);
        }
        if (loading) loadingPanel.Appear();
        yield return new WaitForSeconds(TRANSITION_DURATION);
        transitionImg.transform.DOScale(0, TRANSITION_DURATION);
    }
}
