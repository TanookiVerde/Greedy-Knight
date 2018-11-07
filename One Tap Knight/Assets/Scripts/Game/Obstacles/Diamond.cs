using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour
{
    public float timeToReachPlayer;
    public static int totalDiamonds;
    public static int notCollectedDiamonds;

    private void Start()
    {
        Diamond.totalDiamonds++;
        Diamond.notCollectedDiamonds = Diamond.totalDiamonds;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.DOMove(collision.gameObject.transform.position, timeToReachPlayer);
            collision.gameObject.GetComponent<KnightController>().GetDiamond();
            Diamond.notCollectedDiamonds--;
            Destroy(gameObject, timeToReachPlayer);
        }
    }
}
