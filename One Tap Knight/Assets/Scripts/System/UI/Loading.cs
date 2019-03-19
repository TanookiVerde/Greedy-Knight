using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Loading : MonoBehaviour {
    public float minTime;
    public TMP_Text loadingText;

	private void Start () {
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        StartCoroutine(LoadingAnimation());
        StartCoroutine(LoadingTextAnimation());
	}
    private IEnumerator LoadingAnimation()
    {
        float timer = 0;
        while(timer < minTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        Transition.transition.TransiteTo("Level");
    }
    private IEnumerator LoadingTextAnimation()
    {
        int i = 0;
        while (true)
        {
            string s = "Carregando";
            for (int j = 0; j < i; j++)
                s += ".";
            loadingText.text = s;
            yield return new WaitForSeconds(0.5f);
            i++;
            if (i > 3) i = 0;
        }
    }
}
