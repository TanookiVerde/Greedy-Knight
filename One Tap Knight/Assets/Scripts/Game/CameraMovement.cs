using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour {
	[Header("Preferences")]
	[SerializeField] private GameObject goToFollow;
	[SerializeField] private float cameraTax;
	[SerializeField] private float cameraMaxVelocity;

	private float initialSize;
	[SerializeField] private float size;
	[SerializeField] private CharacterMovement character;

	private bool canFollow = true;

	private void Start(){
		//initialSize = Camera.main.orthographicSize;
		initialSize = Camera.main.fieldOfView;
		StartCoroutine(InitialAnimation());
	}
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
	public IEnumerator InitialAnimation(){
		canFollow = false;
		//Camera.main.DOOrthoSize(size,0);
		Camera.main.DOFieldOfView(size,0);
		this.transform.DOLocalMove(character.transform.position-Vector3.forward*10, 0f);
		yield return new WaitForSeconds(1f);
		this.transform.DOLocalMove(goToFollow.transform.position-Vector3.forward*10, 2f);
		//Camera.main.DOOrthoSize(initialSize,2f);
		Camera.main.DOFieldOfView(initialSize,2f);
		yield return new WaitForSeconds(2f);
		CharacterMovement.canStart = true;
		canFollow = true;
	}
}
