using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour, IScreen {
    [SerializeField] private List<GameObject> objectsToAnimate;

    private const float MAX_SCALE = 1.1f;
    private const float ANIM_DURATION = 0.5f;
    private const float OBJ_DELAY = 0.25f;

    public void Prepare() { }

    public void Close() { }

    public IEnumerator BeginningAnimation()
    {
        foreach(GameObject go in objectsToAnimate)
        {
            Sequence s = DOTween.Sequence();
            s.Append(go.transform.DOScale(MAX_SCALE, ANIM_DURATION / 2));
            s.Append(go.transform.DOScale(1, ANIM_DURATION / 2));
            yield return new WaitForSeconds(OBJ_DELAY);
        }
    }
}
