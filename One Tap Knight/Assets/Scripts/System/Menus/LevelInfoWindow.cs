using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoWindow : MonoBehaviour {
    [SerializeField] private Text levelName;
    [Header("Stars")]
    [SerializeField] private Image normalComplete;
    [SerializeField] private Image allCoinsComplete;
    [SerializeField] private Image noCoinsComplete;
}
