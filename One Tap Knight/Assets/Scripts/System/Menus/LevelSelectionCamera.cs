using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelSelectionCamera : MonoBehaviour {

    [SerializeField] private float timeToMove;
    [SerializeField] private Transform[] cameraAreas;

	public void MoveCamera()
    {
        transform.DOMoveX(cameraAreas[GetCurrentArea()].position.x, timeToMove);
    }
    private int GetCurrentArea()
    {
        return World.selectedLevel/3;
    }
    public IEnumerator SetCameraHeight(float height, float duration)
    {
         transform.DOMoveY(height, duration);
         yield return new WaitForSeconds(duration);
    }
}
