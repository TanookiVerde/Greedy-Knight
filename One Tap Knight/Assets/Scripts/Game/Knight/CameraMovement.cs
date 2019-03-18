using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
    public float initialSize;
    private float finalSize;

    public float sleepyTime;
    public float duration;
    public float smoothSpeed = 0.225f;
    public float yOffset;

    private Vector3 offset;
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
    private void LateUpdate()
    {
        if (canFollow && knight != null)
            FollowPlayer();
    }
    private void FixedUpdate()
    {
        if (canFollow && knight != null)
            FollowPlayer();
    }
    private void FollowPlayer()
    {
        transform.position = new Vector3(knight.position.x - offset.x, transform.position.y, transform.position.z);
    }
    public IEnumerator StartAnimation(bool fast = false)
    {
        yield return new WaitForSeconds(duration * (fast ? 0.5f : 1f));
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
        offset += Vector3.up * distanceLookDown;
    }
    public void ResetLook()
    {
        offset -= Vector3.up * distanceLookDown;
    }
}
