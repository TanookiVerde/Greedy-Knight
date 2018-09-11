using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Cutscene/Scene")]
public class CutScene : ScriptableObject {
	public List<Slide> slides;
	public string sceneName;
}

