using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour {

    [SerializeField] private float unlockAnimationDuration = 0.2f;
    [SerializeField] private GameObject infoPanel;

    public static float initialScale = 1;

    public void Start(){
        initialScale = transform.localScale.x;
    }
    public Vector3 GetPointPosition()
    {
        return transform.position;
    }
    public void UnlockAnimation()
    {
        float initialScale = transform.localScale.x;
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOScale(initialScale * 1.4f, unlockAnimationDuration));
        s.Append(transform.DOScale(initialScale, unlockAnimationDuration));
        s.Play();
    }
    public Tweener GrowAnimation(){
        return transform.DOScale(initialScale * 1.4f, unlockAnimationDuration);
    }
    public Tweener ShortenAnimation(){
        return transform.DOScale(initialScale, unlockAnimationDuration);
    }
}
