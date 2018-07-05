using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="LevelData")]
public class Level : ScriptableObject {
	public string title;
	public Sprite img;
	public string sceneName;
}
