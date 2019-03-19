using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour {
    private const float SCREEN_WIDTH = 1280;
    private const float SCREEN_HEIGHT = 720;

    public List<Submenu> submenus;
    public int currentSubmenu;
    
    public TMP_Text deaths;
    public AudioSource source;

    private void Start()
    {
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        Application.targetFrameRate = 60;
        deaths.text = "MORTES :" + MemoryCard.Load().deaths;
        foreach (var s in submenus)
        {
            s.FastClose();
        }
        submenus[currentSubmenu].Open();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentSubmenu != 0)
        {
            FindObjectOfType<SoundPlayer>().PlaySound();
            OpenSubmenu(0);
        }
    }
    public void OpenSubmenu(int index)
    {
        submenus[currentSubmenu].Close();
        currentSubmenu = index;
        submenus[currentSubmenu].Open();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
