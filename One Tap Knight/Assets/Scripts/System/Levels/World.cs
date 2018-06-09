using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform playerIcon;
    [SerializeField] private LineRenderer lineProgression;

    private void Start()
    {
        UpdateLineProgressionPosition();
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
    private void UpdateLineProgressionPosition()
    {
        lineProgression.SetPosition(1, levels[GetLastUnlockedLevel()].GetPointPosition());
    }

}
