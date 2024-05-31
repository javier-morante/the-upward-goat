using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour,IDataPersistence<SettingsData>
{
        [SerializeField] private TextMeshProUGUI optionsText;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider mainVolume;
        [SerializeField] private Slider enviromentVolume;
        [SerializeField] private Slider sfxVolume;
        
        private string[] options = {"Full Screen","Window"};
        private int currentOption;

        void Start(){
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = (int) Screen.currentResolution.refreshRateRatio.value;
        }

        void Update(){
            OptionExec();
            audioMixer.SetFloat("masterVolume",mainVolume.value-80);
            audioMixer.SetFloat("enviromentVolume",enviromentVolume.value-80);
            audioMixer.SetFloat("sfxVolume",sfxVolume.value-80);
        }
    
        public void Next(){
            currentOption +=1;
            if(currentOption >= options.Length){
                currentOption = 0;
            }
        }
        public void Previous(){
            currentOption -=1;
            if(currentOption < 0){
                currentOption = options.Length-1;
            }
        }

        public void SetMainVolume(float volume){
            audioMixer.SetFloat("masterVolume",volume-80);
        }  
        public void SetEnviromentVolume(float volume){
            audioMixer.SetFloat("enviromentVolume",volume-80);
        }  
        public void SetSFXVolume(float volume){
            audioMixer.SetFloat("sfxVolume",volume-80);
        }  

        void OptionExec(){
            optionsText.text = options[currentOption];
            switch (currentOption)
            {
                case 0:
                    Screen.fullScreen = true;
                break;
                case 1:
                    Screen.fullScreen = false;
                break;
            }
        }

    public void LoadData(SettingsData data)
    {
        this.mainVolume.value = data.mainVolume;
        this.enviromentVolume.value = data.enviromentVolume;
        this.sfxVolume.value = data.enviromentVolume;
        this.currentOption = data.screenOption;
    }

    public void SaveData(ref SettingsData data)
    {
        data.mainVolume = this.mainVolume.value;
        data.enviromentVolume = this.enviromentVolume.value;
        data.sfxVolume = this.sfxVolume.value;
        data.screenOption = currentOption;
    }
}
