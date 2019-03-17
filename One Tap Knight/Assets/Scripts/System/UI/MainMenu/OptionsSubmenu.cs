using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSubmenu : Submenu{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    protected override void OnOpen()
    {
        var log = MemoryCard.Load();
        print(log.musicVolume);
        musicSlider.wholeNumbers = true;
        musicSlider.maxValue = 10;
        musicSlider.minValue = 0;
        musicSlider.value = log.musicVolume;
        sfxSlider.wholeNumbers = true;
        sfxSlider.maxValue = 10;
        sfxSlider.minValue = 0;
        sfxSlider.value = log.sfxVolume;
    }
    protected override void OnClose()
    {
        var log = MemoryCard.Load();
        log.musicVolume = (int) musicSlider.value;
        log.sfxVolume = (int) sfxSlider.value;
        MemoryCard.Save(log);
    }
    public void SaveSoFar()
    {
        OnClose();
    }
    public void ResetSave()
    {
        MemoryCard.Save(new AdventureLog());
    }
}
