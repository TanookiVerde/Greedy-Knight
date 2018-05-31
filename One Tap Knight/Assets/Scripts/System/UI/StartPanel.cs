using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel: MonoBehaviour {

	[SerializeField] private float duration;
    [SerializeField] private float amplitude;

	private void Start()
	{
        SetActiveInstant(false);
	}
	private IEnumerator AnimationLoop()
	{
		while(true){
            transform.DOScale(amplitude, duration/2);
			yield return new WaitForSeconds(duration/2);
            transform.DOScale(1, duration/2);
            yield return new WaitForSeconds(duration/2);
		}
	}
    public void SetActive(bool active)
    {
        if (active)
        {
            GetComponent<Text>().DOFade(1, duration);
            StartCoroutine(AnimationLoop());
        }
        else
        {
            GetComponent<Text>().DOFade(0, duration);
            StopCoroutine(AnimationLoop());
        }
    }
    private void SetActiveInstant(bool active)
    {
        if (active)
        {
            GetComponent<Text>().DOFade(1, 0);
            StartCoroutine(AnimationLoop());
        }
        else
        {
            GetComponent<Text>().DOFade(0, 0);
            StopCoroutine(AnimationLoop());
        }
    }
}
