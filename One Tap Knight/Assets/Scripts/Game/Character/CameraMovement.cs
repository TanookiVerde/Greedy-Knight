using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
	[Header("Preferences")]
	private GameObject target;
    [SerializeField] private float cameraVerticalOffset;

	private bool canFollow = true;

	private void FixedUpdate()
	{
		if(target != null && canFollow) Follow();
	}
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
	private void Follow()
	{
		Vector3 targetPosition = target.transform.position;
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
    }
}
