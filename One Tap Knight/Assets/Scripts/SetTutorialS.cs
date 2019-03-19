using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTutorialS : MonoBehaviour
{
    public void SetTutorial(bool value)
    {
        if(PlayerPrefs.GetInt("tutorial", 1) == 1)
        {
            PlayerPrefs.SetInt("tutorial", 0);
            print(PlayerPrefs.GetInt("tutorial", 1));
        }
        else
        {
            PlayerPrefs.SetInt("tutorial", 1);
            print(PlayerPrefs.GetInt("tutorial", 1));
        }
    }
}
