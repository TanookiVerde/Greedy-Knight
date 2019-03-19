using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    private CameraMovement cameraMovement;
    private KnightController knight;
    private PausePanel pausePanel;
    private EndPanel endPanel;
    private GameOverPanel gameOverPanel;
    private Timer timer;
    [SerializeField] private List<GameObject> detailImages;
    [SerializeField] private CanvasGroup tutorialScreen;
    public bool showFinalCutscene;

    public SettingsSO settings;

    private void Start()
    {
        print(PlayerPrefs.GetInt("tutorial", 1));
        foreach (var img in detailImages)
            img.SetActive(true);
        knight = FindObjectOfType<KnightController>();
        endPanel = FindObjectOfType<EndPanel>();
        pausePanel = FindObjectOfType<PausePanel>();
        gameOverPanel = FindObjectOfType<GameOverPanel>();
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        timer = FindObjectOfType<Timer>();
        GameObject.Find("Toggle").GetComponent<Toggle>().isOn = PlayerPrefs.GetInt("tutorial", 1) == 1 ? true:false;
        StartCoroutine(LevelLoop());
    }
    private IEnumerator LevelLoop()
    {
        bool showTutorial = PlayerPrefs.GetInt("tutorial") == 1 ? true : false;
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        print(showTutorial);
        if (showTutorial)
            yield return ShowTutorial();
        else
            yield return cameraMovement.StartAnimation(showTutorial);
        cameraMovement.StartFollowing();
        while (!LevelFinished())
        {
            if(!IsGamePaused())
            {
                if (!IsPlayerAlive())
                {
                    yield return new WaitForSeconds(1f);
                    gameOverPanel.Show();
                    yield break;
                }
                knight.MovementLoop();
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SaveProgress();
        if (showFinalCutscene)
        {
            PlayerPrefs.SetInt("cutscene", 1);
            Transition.transition.TransiteTo("Cutscene");
        }
        else
        {
            endPanel.Show();
        }
    }
    
    private bool IsGamePaused() { return pausePanel.paused; }
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
    private IEnumerator ShowTutorial()
    {
        tutorialScreen.DOFade(1, 0);
        yield return new WaitForSeconds(1f);
        tutorialScreen.DOFade(0, 1f);
        yield return new WaitForSeconds(1f);
    }
}
