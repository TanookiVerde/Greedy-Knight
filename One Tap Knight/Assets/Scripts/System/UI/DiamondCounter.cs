using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiamondCounter : MonoBehaviour {

    public TMP_Text value;
    public TMP_Text maxValue;

    private void Start()
    {
        ResetCounter();
    }
    public void UpdateDiamondCounter()
    {
        value.text = Diamond.collectedDiamonds.ToString();
        maxValue.text = Diamond.totalDiamonds.ToString();
    }
    private void ResetCounter()
    {
        Diamond.totalDiamonds = 0;
        Diamond.collectedDiamonds = 0;
        UpdateDiamondCounter();
    }
}
