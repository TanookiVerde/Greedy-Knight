using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/New Level")]
public class SOLevel : ScriptableObject {
    public string title;
    public int diamonds;
    public bool playFinalCutscene;
    public Sprite image;
    public AudioClip backgroundSound;
}
