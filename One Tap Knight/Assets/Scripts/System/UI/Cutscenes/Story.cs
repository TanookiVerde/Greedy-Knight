using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Story")]
public class Story : ScriptableObject {
    public List<Scene> scenes;
}
