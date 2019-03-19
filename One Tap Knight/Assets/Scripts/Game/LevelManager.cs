using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    public SOGame gameData;
    public List<GameObject> levels;
    public int levelIndex;

    private CameraMovement cameraMovement;
    private KnightController knight;
    [SerializeField] CanvasGroup waitingPanel;
    
    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt("levelNumber", 0);
        OpenLevelMap(levelIndex);
        knight = FindObjectOfType<KnightController>();
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        StartCoroutine(LevelLoop());
    }
    private IEnumerator LevelLoop()
    {
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        yield return null;
        FindObjectOfType<MusicManager>().PlayMusic(levelIndex);
        yield return WaitForInput();
        cameraMovement.StartFollowing();
        while (!LevelFinished())
        {
            if(!IsGamePaused())
            {
                if (!IsPlayerAlive())
                {
                    var log = MemoryCard.Load();
                    log.deaths++;
                    MemoryCard.Save(log);
                    yield return new WaitForSeconds(1f);
                    Transition.transition.TransiteTo(PlayerPrefs.GetString("levelName"));
                    yield break;
                }
                knight.MovementLoop();
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SaveProgress();
        if (gameData.levels[levelIndex].playFinalCutscene)
        {
            FindObjectOfType<MusicManager>().source.DOFade(0, 0.5f);
            Destroy(FindObjectOfType<MusicManager>().gameObject);
            PlayerPrefs.SetInt("cutscene", 1);
            Transition.transition.TransiteTo("Cutscene");
        }
        else
        {
            cameraMovement.EndAnimation();
            FindObjectOfType<EndPanel>().Show();
        }
    }
    private bool IsGamePaused() { return FindObjectOfType<PausePanel>().paused; }
    private bool IsPlayerAlive() { return knight != null; }
    private bool LevelFinished() { return knight.finishedLevel; }
    private void SaveProgress()
    {
        var log = MemoryCard.Load();
        int selectedLevel = PlayerPrefs.GetInt("levelNumber");
        if(Diamond.collectedDiamonds > log.levels[selectedLevel].diamondsCollected)
        {
            log.levels[selectedLevel].diamondsCollected = Diamond.collectedDiamonds;
        }
        if (!log.levels[selectedLevel].completed)
        {
            log.levels[selectedLevel].completed = true;
        }
        MemoryCard.Save(log);
    }
    private void OpenLevelMap(int levelIndex)
    {
        foreach(var level in levels)
        {
            level.SetActive(false);
        }
        levels[levelIndex].SetActive(true);
    }
    private IEnumerator WaitForInput()
    {
        waitingPanel.DOFade(1, 0);
        while (!Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.S))
        {
            yield return null;
        }
        waitingPanel.DOFade(0, 0.2f);
    }
}
