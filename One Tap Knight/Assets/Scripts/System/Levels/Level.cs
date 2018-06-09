using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    [Header("Level Information")]
    [SerializeField] private bool isLocked;
    [SerializeField] private int coinsAmount;
    [Header("Player Information")]
    [SerializeField] private int coinsAcquired;
    [SerializeField] private bool gotAllCoins;
    [SerializeField] private bool gotNoCoins;

    public bool IsLocked()
    {
        return isLocked;
    }
    public Vector3 GetPointPosition()
    {
        return transform.position;
    }
    public void UnlockLevel()
    {
        isLocked = false;
    }
    public void GotAllCoins()
    {
        gotAllCoins = false;
    }
    public void GotNoCoins()
    {
        gotNoCoins = true;
    }
    public string SerializeLevelData()
    {
        return JsonUtility.ToJson(this);
    }
    public void DeserializeLevelDataToThisObject(string levelData)
    {
        Level data = JsonUtility.FromJson<Level>(levelData);
        this.isLocked = data.isLocked;
        this.gotAllCoins = data.gotAllCoins;
        this.gotNoCoins = data.gotNoCoins;
        this.coinsAcquired = data.coinsAcquired;
    }
}
