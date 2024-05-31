using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
   public float mainVolume;

   public float enviromentVolume;

   public float sfxVolume;

   public int screenOption;

   public SettingsData(){

    mainVolume = 50;
    enviromentVolume = 50;
    enviromentVolume = 50;
    screenOption = 0;
   }
}
