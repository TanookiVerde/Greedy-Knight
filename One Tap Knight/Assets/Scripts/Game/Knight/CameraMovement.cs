using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public float initialSize;
    private float finalSize;

    public float sleepyTime;
    public float duration;
    public float smoothSpeed = 0.125f;

    private Vector2 offset;
    public float distanceLookDown;

    private Transform knight;
    private new Camera camera;

    private bool canFollow = false;


    private void Start()
    {
        knight = FindObjectOfType<KnightController>().transform;
        camera = FindObjectOfType<Camera>();
        finalSize = camera.fieldOfView;
        offset = knight.position - transform.position;
    }
    private void FixedUpdate()
    {
        if(canFollow && knight != null)
            FollowPlayer();
    }
    private void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(knight.position.x, knight.position.y, transform.position.z) - (Vector3) offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        
        transform.position = smoothedPosition;
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
    public void LookDown()
    {
        offset += Vector2.up * distanceLookDown;
    }
    public void ResetLook()
    {
        offset -= Vector2.up * distanceLookDown;
    }
}
