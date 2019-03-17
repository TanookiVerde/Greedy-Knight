using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraLoop : MonoBehaviour {
    public float initialPosition = 1;
    public float finalPosition = 2;
    public float duration;
    public Camera camera;

    private void Start()
    {
        StartCoroutine(Loop());
    }
    private IEnumerator Loop()
    {
        while (true)
        {
            camera.transform.DOMoveX(initialPosition, 0);
            camera.transform.DOMoveX(finalPosition, duration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration);
        }
    }
}
