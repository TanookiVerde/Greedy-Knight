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
        StartCoroutine(AnimationLoop());
        if(FindObjectOfType<DiamondCounter>() != null)
            FindObjectOfType<DiamondCounter>().UpdateDiamondCounter();
    }
    public void ResetDiamonds()
    {
        Diamond.totalDiamonds = 0;
        Diamond.collectedDiamonds = 0;
    }
    private IEnumerator AnimationLoop()
    {
        int dir = -1;
        float time = 0.7f;
        while (true)
        {
            transform.DOMoveY(transform.position.y + dir*0.05f, time);
            yield return new WaitForSeconds(time);
            dir *= -1;
            time = Random.Range(0.3f, 1f);
        }
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
