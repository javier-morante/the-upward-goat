using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The SettingsData class is used to store settings related to the game's audio and display options.
[System.Serializable]
public class SettingsData
{
   public float mainVolume;

   public float enviromentVolume;

   public float sfxVolume;

   public int screenOption;

   public bool jumpChargeBar;

   public SettingsData(){
    mainVolume = 50;
    enviromentVolume = 50;
    enviromentVolume = 50;
    screenOption = 0;
     jumpChargeBar = false;
   }
}
