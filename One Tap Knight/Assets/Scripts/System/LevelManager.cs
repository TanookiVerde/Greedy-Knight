using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] private int collectedCoins;

    [Header("Panels")]
    [SerializeField] private StartPanel startTextPanel;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private EndLevelPanel endLevelPanel;

    private Character player;

    private void Start()
    {
        GetPlayer();
        StartCoroutine(LevelState());
    }
    private IEnumerator LevelState()
    {
        startTextPanel.SetActive(true);
        yield return WaitForPlayerInitialInput();
        startTextPanel.SetActive(false);
        while (player != null && !player.FinishedLevel())
        {
            player.Action();
            yield return null;
        }
        if (player.FinishedLevel())
            endLevelPanel.SetActive(true);
        else if (player == null)
            gameOverPanel.SetActive(true);
        Coin.ResetTotalCoin();
    }
    private IEnumerator WaitForPlayerInitialInput()
    {
        while (!Input.GetMouseButton(0)) yield return null;
    }
    private void GetPlayer()
    {
        player = GameObject.FindObjectOfType<Character>();
    }
}
