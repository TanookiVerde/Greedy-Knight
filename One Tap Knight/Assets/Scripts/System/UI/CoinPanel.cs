using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinPanel : MonoBehaviour {

    [Header("Editor")]
    [SerializeField] private RectTransform canvasRoot;
    [SerializeField] private GameObject coinHudPrefab;

    [SerializeField] private List<Image> coinList;

    private int lastCoinCollected;

    private void Start()
    {
        for(int i = 0; i < Coin.totalCoin; i++)
        {
            GameObject coin = Instantiate(coinHudPrefab, canvasRoot.transform);
            coinList.Add(coin.GetComponent<Image>());
        }
        foreach(Image coin in coinList)
        {
            coin.DOFade(0.3f, 0);
            coin.transform.DOScale(0.9f, 0);
        }
    }
    public void CoinCollected()
    {
        coinList[lastCoinCollected].DOFade(1, 0.5f);
        coinList[lastCoinCollected].transform.DOScale(1f, 0.5f);
        lastCoinCollected++;
    }
}
