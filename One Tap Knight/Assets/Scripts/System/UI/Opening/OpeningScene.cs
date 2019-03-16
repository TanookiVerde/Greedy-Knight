using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour {

    [SerializeField] private Image gdpNormalLogo;
    [SerializeField] private Image gdpCustomizedLogo;

    private void Start()
    {
        StartCoroutine(Animation());
    }
    private IEnumerator Animation()
    {
        gdpNormalLogo.DOFade(0, 0);
        gdpCustomizedLogo.DOFade(0, 0);
        yield return new WaitForSeconds(1f);
        gdpNormalLogo.DOFade(1, 1f);
        yield return new WaitForSeconds(3f);
        gdpNormalLogo.DOFade(0, 1f);
        gdpCustomizedLogo.DOFade(1, 1f);
        yield return new WaitForSeconds(3f);
        gdpCustomizedLogo.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        Transition.transition.TransiteTo("Cutscene");
    }
}
