using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    
    void Start()
    {
        if (DataPersistanceManager.instance.HasGameData())
        {
            GameData gameData = DataPersistanceManager.instance.GetGameData();
            TimeSpan tmpTime = TimeSpan.FromSeconds(10000);
            string a = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",tmpTime.Hours,tmpTime.Minutes,tmpTime.Seconds,tmpTime.Milliseconds);
            

            string[] sentences = {
                "Game Time: "+a,
                "Jumps made: "+gameData.jumpCount,
                "Coins collected: "+gameData.coins
            };
            FindObjectOfType<DialogManager>().StartDialog(sentences);
        }
    }
}
