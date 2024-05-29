using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour,IObserver<PlayerEvents>,IDataPersistence
{
    private int coinCount;
    private int jumpCounter;
    [SerializeField] private TextMeshProUGUI cronometerText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI jumpText;

    private float time;

    public void LoadData(GameData gameData)
    {
        this.jumpCounter = gameData.jumpCount;
        this.time = gameData.cronometer;
    }

    public void OnNotify(PlayerEvents notification)
    {
        switch (notification)
        {
            case PlayerEvents.CoinCollected:
                coinCount ++;
                break;
            case PlayerEvents.Jump:
                jumpCounter++;
                break;
        }
        
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.jumpCount = this.jumpCounter;
        gameData.cronometer = this.time;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        int mseconds = Mathf.FloorToInt((time % 1)*100);
        int seconds = Mathf.FloorToInt(time % 60);
        int minutes = Mathf.FloorToInt(time / 60);
        int hours = Mathf.FloorToInt(time*60);
        cronometerText.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",TimeSpan.FromSeconds(time).Hours,minutes,seconds,mseconds);

        coinText.text = "Coins:"+coinCount;
        jumpText.text = "Jumps:"+jumpCounter;
    }
}