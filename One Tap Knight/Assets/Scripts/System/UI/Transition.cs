using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour {
    private const float TRANSITION_DURATION = 0.25f;
    private const float TRANSITION_MIN_TIME = 1f;

    public static Transition transition;

    private void Start()
    {
        transition = this;
    }
    public void InstaShow()
    {
        GetComponent<Image>().DOFade(1, 0);
    }
    public void InstaHide()
    {
        GetComponent<Image>().DOFade(0, 0);
    }
    public void TransiteTo(string sceneName) {
        StartCoroutine(TransiteToAnimation(sceneName));
    }
    public void TransiteFrom() {
        GetComponent<Image>().DOFade(0, TRANSITION_DURATION*5);
    }
    private IEnumerator TransiteToAnimation(string name)
    {
        GetComponent<Image>().DOFade(1, TRANSITION_DURATION);
        yield return new WaitForSeconds(TRANSITION_MIN_TIME);
        SceneManager.LoadSceneAsync(name);
    }
}
