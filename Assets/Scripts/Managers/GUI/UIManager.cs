using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour,IObserver<PlayerEvents>,IDataPersistence<GameData>
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
        this.coinCount = gameData.coinsCollected;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.jumpCount = this.jumpCounter;
        gameData.cronometer = this.time;
        gameData.coinsCollected = this.coinCount;
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


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        cronometerText.text = formatStringToTime(time);

        coinText.text = "Coins:"+coinCount;
        jumpText.text = "Jumps:"+jumpCounter;
    }

    public static string formatStringToTime(float time){
        TimeSpan tmpTime = TimeSpan.FromSeconds(time);
            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", 
                                                tmpTime.Hours, 
                                                tmpTime.Minutes, 
                                                tmpTime.Seconds, 
                                                tmpTime.Milliseconds/10);
    }
}
