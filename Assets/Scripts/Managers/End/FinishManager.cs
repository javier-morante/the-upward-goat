using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    
    void Start()
    {
        Time.timeScale = 1;
        CursorManager.HideCursor(true);
        
        if (DataPersistanceManager.instance.HasGameData())
        {
            
            GameData gameData = DataPersistanceManager.instance.GetGameData();
            string[] sentences = {
                "Game Time: "+UiManager.formatStringToTime(gameData.cronometer),
                "Jumps made: "+gameData.jumpCount,
                "Coins collected: "+gameData.coinsCollected
            };
            FindObjectOfType<DialogManager>().StartDialog(sentences);
        }
    }

    public void ToMenu(){
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Menu);
    }
}
