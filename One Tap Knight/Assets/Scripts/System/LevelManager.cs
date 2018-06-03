using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    [Header("Stats")]
    [SerializeField] public static int collectedCoins;

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
        while (IsPlayerAlive() && !IsLevelFinished())
        {
            player.Action();
            yield return null;
        }
        if (!IsPlayerAlive())
        {
            gameOverPanel.SetActive(true);
        } else if (IsLevelFinished())
        {
            player.Finish();
            yield return new WaitForSeconds(player.timeToFinish);
            endLevelPanel.SetActive(true);
        }
        Coin.ResetTotalCoin();
        collectedCoins = 0;
    }
    private IEnumerator WaitForPlayerInitialInput()
    {
        while (!Input.GetMouseButton(0)) yield return null;
    }
    private void GetPlayer()
    {
        player = GameObject.FindObjectOfType<Character>();
    }
    private bool IsPlayerAlive()
    {
        return player != null;
    }
    private bool IsLevelFinished()
    {
        return player.FinishedLevel();
    }
}
