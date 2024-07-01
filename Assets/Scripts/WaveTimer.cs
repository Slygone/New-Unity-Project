using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI waveText;
    public float currentTime;
    public float startingTime = 180f;
    public float nextWaveTime = 180f;
    public float currentWave = 0;

    
    
    void Start()
    {
        currentTime = startingTime;
    }

    
    void Update()
    {
        currentTime -= Time.deltaTime;
        UpdateTimerDisplay(currentTime,currentWave);
        if(currentTime <= 0)
        {
            TimerEnds();
        }
    }
    //updating Text with current time and wave count
    void UpdateTimerDisplay(float time, float wave)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("Next Wave in: {0:00}:{1:00}", minutes, seconds);
        waveText.text = "Wave: " + currentWave;
    }

    void TimerEnds()
    {
        //setting timer for next wave
        currentTime = nextWaveTime;
        //incremeting wave count
        currentWave++;
    }
}
