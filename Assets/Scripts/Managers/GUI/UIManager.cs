using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour, IObserver<PlayerEvents>, IDataPersistence<GameData>
{
    // Count of collected coins
    private int coinCount;
    // Count of jumps made
    private int jumpCounter;
    // Text element to display the elapsed time
    [SerializeField] private TextMeshProUGUI cronometerText;
    // Text element to display the number of coins collected
    [SerializeField] private TextMeshProUGUI coinText;
    // Text element to display the number of jumps made
    [SerializeField] private TextMeshProUGUI jumpText;

    // Elapsed time since the game started
    private float time;

    // Load game data
    public void LoadData(GameData gameData)
    {
        this.jumpCounter = gameData.jumpCount;
        this.time = gameData.cronometer;
        this.coinCount = gameData.coinsCollected;
    }

    // Save game data
    public void SaveData(ref GameData gameData)
    {
        gameData.jumpCount = this.jumpCounter;
        gameData.cronometer = this.time;
        gameData.coinsCollected = this.coinCount;
    }

    // Handle notifications from the player events
    public void OnNotify(PlayerEvents notification)
    {
        switch (notification)
        {
            case PlayerEvents.CoinCollected:
                coinCount++;
                break;
            case PlayerEvents.Jump:
                jumpCounter++;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update elapsed time
        time += Time.deltaTime;
        // Display the elapsed time in the specified format
        cronometerText.text = "Time: "+FormatStringToTime(time);
        // Update coin count text
        coinText.text = "Coins: " + coinCount;
        // Update jump count text
        jumpText.text = "Jumps: " + jumpCounter;
    }

    // Format the elapsed time into a string format
    public static string FormatStringToTime(float time)
    {
        TimeSpan tmpTime = TimeSpan.FromSeconds(time);
        return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", 
                             tmpTime.Hours, 
                             tmpTime.Minutes, 
                             tmpTime.Seconds, 
                             tmpTime.Milliseconds / 10);
    }
}

