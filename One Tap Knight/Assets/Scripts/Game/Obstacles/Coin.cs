﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Coin : MonoBehaviour {
	[SerializeField] private float timeToDestroy = 0.5f;

	[SerializeField] private Text coinText;

	public static int totalCoin;

	private void Start()
	{
		coinText = GameObject.Find("CoinText").GetComponent<Text>();
		totalCoin++;
		UpdateTextUI();
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Character.coinInLevel++;
			DestroyCoin();
			UpdateTextUI();
        }
    }
	private void DestroyCoin()
	{
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
	private void UpdateTextUI()
	{
		coinText.text = "COINS: " + Character.coinInLevel + "/" + totalCoin;
	}
}
