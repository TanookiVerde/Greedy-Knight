using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public float initialSize;
    private float finalSize;

    public float sleepyTime;
    public float duration;

    public float cameraHorizontalOffset;

    private Transform knight;
    private new Camera camera;

    private bool canFollow = false;

    private void Start()
    {
        knight = FindObjectOfType<KnightController>().transform;
        camera = FindObjectOfType<Camera>();
        finalSize = camera.fieldOfView;
    }
    private void LateUpdate()
    {
        if(canFollow && knight != null)
            FollowPlayer();
    }
    private void FollowPlayer()
    {
        transform.position = new Vector3(knight.position.x - cameraHorizontalOffset, transform.position.y, transform.position.z);
    }
    public IEnumerator StartAnimation()
    {
        FollowPlayer();
        var pos = camera.transform.position;
        camera.DOFieldOfView(initialSize, 0);
        camera.transform.position = new Vector3(knight.transform.position.x, knight.transform.position.y, camera.transform.position.z);
        yield return new WaitForSeconds(sleepyTime);
        camera.DOFieldOfView(finalSize, duration);
        camera.transform.DOMove(pos, duration);
        yield return new WaitForSeconds(duration);
    }
    public void StartFollowing()
    {
        canFollow = true;
    }
    public void Bounce()
    {
        Sequence s = DOTween.Sequence();
        s.Append(camera.DOFieldOfView(finalSize * 1.1f, 0.5f));
        s.Append(camera.DOFieldOfView(finalSize, 0.5f));
    }
}
