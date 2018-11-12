using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour
{
    public float timeToReachPlayer;
    public static int totalDiamonds;
    public static int collectedDiamonds;

    private void Start()
    {
        Diamond.totalDiamonds++;
        Diamond.collectedDiamonds = 0;
        if(FindObjectOfType<DiamondCounter>() != null)
            FindObjectOfType<DiamondCounter>().UpdateDiamondCounter();
    }
    public void ResetDiamonds()
    {
        Diamond.totalDiamonds = 0;
        Diamond.collectedDiamonds = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.DOMove(collision.gameObject.transform.position, timeToReachPlayer);
            transform.DOScale(0, timeToReachPlayer);
            Diamond.collectedDiamonds++;
            FindObjectOfType<DiamondCounter>().UpdateDiamondCounter();
            Destroy(gameObject, timeToReachPlayer);
        }
    }
}
