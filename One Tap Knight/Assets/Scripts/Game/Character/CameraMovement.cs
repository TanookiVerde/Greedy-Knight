using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
	[Header("Preferences")]
	public GameObject goToFollow;
	[SerializeField] private float smoothSpeed = 0.125f;
	[SerializeField] private float cameraMaxVelocity;
	[SerializeField] private float cameraTax;

	private bool canFollow = true;

	private void FixedUpdate()
	{
		if(goToFollow != null && canFollow) Follow();
	}
	private void FollowByLerp()
	{
		var targetPosition = goToFollow.transform.position + Vector3.forward*(-10);
		var smoothedPosition = Vector3.Lerp(transform.position,targetPosition,smoothSpeed*Time.deltaTime);
		transform.position = smoothedPosition;
	}
	private bool Follow()
	{
		Vector3 go_position = goToFollow.transform.position;
		go_position.z = transform.position.z;
		float velocity = (go_position - transform.position).magnitude/cameraTax;
		transform.position = Vector3.MoveTowards(transform.position, 
			go_position, 
			Mathf.Clamp(velocity,-cameraMaxVelocity,cameraMaxVelocity));
		return transform.position == goToFollow.transform.position;
}
	public IEnumerator ShowTarget(Vector3 target_position, float going_duration, float staying_duration)
	{
		canFollow = false;
		this.transform.DOLocalMove(target_position, going_duration);
		yield return new WaitForSeconds(staying_duration);
		canFollow = true;
	}
	public void InstantMovement(Vector3 target_position)
	{
		this.transform.DOLocalMove(target_position,0);
	}
}
