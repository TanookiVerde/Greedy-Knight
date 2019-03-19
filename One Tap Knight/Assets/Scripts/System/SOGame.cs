using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/New Game")]
public class SOGame : ScriptableObject {
    public List<SOLevel> levels;
}
