using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    [Header("Music Preferences")]
    [SerializeField] private float gameOverPitch;
    [SerializeField] private MusicHandler musicHandler;

    [Header("Stats")]
    [SerializeField] public static int collectedCoins;

    [Header("Panels")]
    [SerializeField] private StartPanel startTextPanel;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private EndLevelPanel endLevelPanel;

    [Header("Player")]
    [SerializeField] private Transform playerInitialPosition;
    [SerializeField] private GameObject playerPrefab;

    private Character player;
    private PausePanel pause;

    private void Start()
    {
        //Application.targetFrameRate = 30;
        CreateAndGetPlayer();
        GetPause();
        StartCoroutine(LevelState());
    }
    private IEnumerator LevelState()
    {
        gameOverPanel.DisableGameOverPanel();
        startTextPanel.SetActive(true);
        yield return WaitForPlayerInitialInput();
        startTextPanel.SetActive(false);
        FindObjectOfType<AudioHandler>().PlayEffect(4);
        while (IsPlayerAlive() && !IsLevelFinished())
        {
            if(!IsGamePaused())
                player.Action();
            yield return new WaitForFixedUpdate();
        }
        if (!IsPlayerAlive())
        {
            musicHandler.ChangePitch(gameOverPitch);
            StartCoroutine( gameOverPanel.Appear() );
        } else if (IsLevelFinished())
        {
            SaveAndLoad.FinishAndSaveLevel(collectedCoins == Coin.totalCoin, collectedCoins == 0);
            player.Stop();
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
    private void CreateAndGetPlayer()
    {
        player = Instantiate(playerPrefab, playerInitialPosition.position, Quaternion.identity).GetComponent<Character>();
        Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        print(player.transform.GetChild(1).gameObject);
        Camera.main.GetComponent<CameraMovement>().SetTarget(player.transform.GetChild(1).gameObject);
    }
    private void GetPause(){
        pause = FindObjectOfType<PausePanel>();
    }
    private bool IsPlayerAlive()
    {
        return player != null;
    }
    private bool IsLevelFinished()
    {
        return player.FinishedLevel();
    }
    private bool IsGamePaused(){
        return pause.paused;
    }
    public void Restart()
    {
        if(player != null)
            player.Die();
    }
}
