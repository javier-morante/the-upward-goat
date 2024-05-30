using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float cronometer;
    public int jumpCount;
    public int coins;   
    public Vector3 playerPosition;

    public Vector3 cameraPosition;

    

    public GameData(){
        jumpCount = 0;
        cronometer = 0f;
        coins = 0;
        playerPosition = Vector3.zero;
        cameraPosition = new Vector3(0,0,-10);
    }

    
}
