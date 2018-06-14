using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class World : MonoBehaviour {
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform playerIcon;
    [SerializeField] private LineRenderer lineProgression;
    [Header("Properties")]
    [SerializeField] private float lineProgressDuration;
    [SerializeField] private float unlockAnimationOffset;

    private int currentLevel;

    private void Start()
    {
        StartCoroutine(ProgressToNextLevel());
    }
    private int GetLastUnlockedLevel()
    {
        for(int i = 0; i < levels.Count; i++)
        {
            if (levels[i].IsLocked())
                return --i;
        }
        return levels.Count;
    }
    private IEnumerator ProgressToNextLevel()
    {
        Vector3 initialPosition = levels[GetLastUnlockedLevel()].transform.position;
        levels[GetLastUnlockedLevel() + 1].UnlockLevel();
        Vector3 finalLinePosition = levels[GetLastUnlockedLevel()].transform.position - initialPosition;
        float increaseRatio = 0;
        DOTween.To(() => increaseRatio, x => increaseRatio = x, 1, lineProgressDuration);
        while(increaseRatio < unlockAnimationOffset)
        {
            lineProgression.SetPosition(1, initialPosition + finalLinePosition * increaseRatio);
            yield return new WaitForEndOfFrame();
        }
        levels[GetLastUnlockedLevel()].UnlockAnimation();
        while (increaseRatio < 1f)
        {
            lineProgression.SetPosition(1, initialPosition + finalLinePosition * increaseRatio);
            yield return new WaitForEndOfFrame();
        }
    }
}
