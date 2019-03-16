using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Scene")]
public class Scene : ScriptableObject {
    public Sprite image;
    public List<string> texts;
}
