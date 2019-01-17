using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    private CameraMovement cameraMovement;
    private KnightController knight;
    private EndPanel endPanel;
    private GameOverPanel gameOverPanel;
    private Timer timer;
    [SerializeField] private List<GameObject> detailImages;
    [SerializeField] private CanvasGroup tutorialScreen;

    private void Start()
    {
        tutorialScreen.gameObject.SetActive(true);
        foreach (var img in detailImages)
            img.SetActive(true);
        knight = FindObjectOfType<KnightController>();
        endPanel = FindObjectOfType<EndPanel>();
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
        yield return cameraMovement.StartAnimation();
        tutorialScreen.DOFade(0, 0.25f);
        cameraMovement.StartFollowing();
        while (!LevelFinished())
        {
            if (!IsPlayerAlive())
            {
                yield return new WaitForSeconds(1f);
                gameOverPanel.Show();
                yield break;
            }
            knight.MovementLoop();
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        print("READED FINISHED");
        endPanel.Show();
    }
    private bool IsPlayerAlive() { return knight != null; }
    private bool LevelFinished() { return knight.finishedLevel; }
}
