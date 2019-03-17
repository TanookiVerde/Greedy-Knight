using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public SettingsSO settings;

    private void Start()
    {
        if(tutorialScreen != null && settings.showTutorial)
            tutorialScreen.gameObject.SetActive(true);
        foreach (var img in detailImages)
            img.SetActive(true);
        knight = FindObjectOfType<KnightController>();
        endPanel = FindObjectOfType<EndPanel>();
        pausePanel = FindObjectOfType<PausePanel>();
        pausePanel.SetTutorialToggle(settings.showTutorial);
        gameOverPanel = FindObjectOfType<GameOverPanel>();
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        timer = FindObjectOfType<Timer>();
        StartCoroutine(LevelLoop());
    }
    private IEnumerator LevelLoop()
    {
        float volume = Camera.main.GetComponent<AudioSource>().volume;
        Camera.main.GetComponent<AudioSource>().DOFade(0, 0);
        Camera.main.GetComponent<AudioSource>().DOFade(volume, 0.5f);
        Camera.main.GetComponent<AudioSource>().Play();
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        yield return cameraMovement.StartAnimation(!settings.showTutorial);
        if(tutorialScreen != null && settings.showTutorial)
            tutorialScreen.DOFade(0, 0.25f);
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
        endPanel.Show();
    }
    private bool IsGamePaused() { return pausePanel.paused; }
    private bool IsPlayerAlive() { return knight != null; }
    private bool LevelFinished() { return knight.finishedLevel; }
    public void SetTutorial(bool value) { settings.showTutorial = value; }
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
}
