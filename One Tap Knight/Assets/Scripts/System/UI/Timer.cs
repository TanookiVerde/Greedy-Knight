using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {

    public TMP_Text timer;
    public TMP_Text record;

    public void StartTimer(int time)
    {
        int minutes = ((int)time) / 60;
        int seconds = ((int)time) % 60;
        int milis = (int)((time - ((int)time)) * 100);
        record.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milis.ToString("00");
        StartCoroutine(TimerCoroutine());
    }
    private IEnumerator TimerCoroutine()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            yield return null;
            int minutes = ((int)time) / 60;
            int seconds = ((int)time) % 60;
            int milis = (int)((time - ( (int)time ))*100);
            timer.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milis.ToString("00");
        }
    }
}
