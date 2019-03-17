using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySubmenu : Submenu {
    [SerializeField] private List<LevelButton> levels;
    [SerializeField] private List<Sprite> levelImages;

    protected override void OnOpen()
    {
        var adventureLog = MemoryCard.Load();
        for(int i = 0; i < adventureLog.levels.Count; i++)
        {
            if (ShouldLevelBeShown(adventureLog.levels, i)){
                levels[i].gameObject.SetActive(true);
                levels[i].SetInfo(adventureLog.levels[i], levelImages[i]);
            }
            else
            {
                levels[i].gameObject.SetActive(false);
            }
        }
    }
    private bool ShouldLevelBeShown(List<Level> levels, int index)
    {
        if (index == 0 || levels[index].completed || levels[index - 1].completed)
            return true;
        else
            return false;
    }
}
