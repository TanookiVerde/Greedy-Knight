using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
	[Header("Preferences")]
	[SerializeField] private GameObject goToFollow;
	[SerializeField] private float cameraTax;
	[SerializeField] private float cameraMaxVelocity;

	private bool canFollow = true;

	private void Update(){
		if(goToFollow != null && canFollow) Follow();
	}
	private bool Follow(){
		Vector3 go_position = goToFollow.transform.position;
		go_position.z = transform.position.z;
		float velocity = (go_position - transform.position).magnitude/cameraTax;
		transform.position = Vector3.MoveTowards(transform.position, 
			go_position, 
			Mathf.Clamp(velocity,-cameraMaxVelocity,cameraMaxVelocity));
		return transform.position == goToFollow.transform.position;
	}
	public IEnumerator ShowTarget(Vector3 target_position, float going_duration, float staying_duration){
		canFollow = false;
		this.transform.DOLocalMove(target_position, going_duration);
		yield return new WaitForSeconds(staying_duration);
		canFollow = true;
	}
	public void InstantMovement(Vector3 target_position){
		this.transform.DOLocalMove(target_position,0);
	}
}
