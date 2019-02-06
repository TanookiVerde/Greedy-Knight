using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Settings")]
public class SettingsSO : ScriptableObject {
[	Header("Information")]
	public string profile;

	[Header("PlayerPrefs")]
	public bool showTutorial;

}
