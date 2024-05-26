using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int jumpCount;
    public float cronometer;
    public Vector3 playerPosition;

    public GameData(){
        playerPosition = Vector3.zero;
        jumpCount = 0;
    }

    
}
