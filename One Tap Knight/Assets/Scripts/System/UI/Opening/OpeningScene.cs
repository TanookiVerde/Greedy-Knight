using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour {
    [SerializeField] private TMPro.TMP_Text alert;
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
        alert.DOFade(0, 0);
        yield return CreateSave();
        gdpNormalLogo.DOFade(1, 1f);
        yield return new WaitForSeconds(3f);
        gdpNormalLogo.DOFade(0, 1f);
        gdpCustomizedLogo.DOFade(1, 1f);
        yield return new WaitForSeconds(3f);
        gdpCustomizedLogo.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
        Transition.transition.TransiteTo("Cutscene");
    }
    private IEnumerator CreateSave()
    {
        var log = MemoryCard.Load();
        if(log == null || log.initialized == false)
        {
            var newLog = new AdventureLog();
            newLog.initialized = true;
            MemoryCard.Save(newLog);
            alert.DOFade(1, 0.5f);
            yield return new WaitForSeconds(3);
            alert.DOFade(0, 0.5f);
            yield return new WaitForSeconds(2);
        }
        else
        {
            print(JsonUtility.ToJson(log));
        }
    }
}
