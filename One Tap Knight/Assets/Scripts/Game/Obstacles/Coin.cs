using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Coin : MonoBehaviour {
	[SerializeField] private float timeToDestroy = 0.5f;
	[SerializeField] private float rotationVelocity = 10;

    [SerializeField] private CoinPanel coinPanel;

    private bool collected;

	public static int totalCoin;

	private void Start()
	{
        coinPanel = GameObject.Find("CoinPanel").GetComponent<CoinPanel>();
		totalCoin++;
	}
	private void Update()
	{
		transform.Rotate(0,rotationVelocity*Time.deltaTime,0);
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!collected) LevelManager.collectedCoins++;
            collected = true;
            coinPanel.CoinCollected();
			DestroyCoin();
        }
    }
	private void DestroyCoin()
	{
		FindObjectOfType<AudioHandler>().PlayEffect(9);
		transform.DOLocalMove(
			Character.myTransform.position,
			timeToDestroy
			);
		transform.DOScale(
			0,
			timeToDestroy
			);
		Destroy(this.gameObject,timeToDestroy);
	}
    public static void ResetTotalCoin()
    {
        Coin.totalCoin = 0;
    }
}
