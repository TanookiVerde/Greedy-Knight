using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour
{
    public float timeToReachPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.DOMove(collision.gameObject.transform.position, timeToReachPlayer);
            collision.gameObject.GetComponent<KnightController>().GetDiamond();
            Destroy(gameObject, timeToReachPlayer);
        }
    }
}
