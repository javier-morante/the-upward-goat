using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
        [SerializeField] TextMeshProUGUI optionsText;
        private string[] options = {"Full Screen","Window"};
        private int currentOption;
        [SerializeField] private AudioMixer audioMixer;

        void Start(){
            currentOption = Screen.fullScreen?1:0;
        }

        void Update(){
            OptionExec();
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
            audioMixer.SetFloat("masterVolume",volume);
        }  
        public void SetEnviromentVolume(float volume){
            audioMixer.SetFloat("enviromentVolume",volume);
        }  
        public void SetSFXVolume(float volume){
            audioMixer.SetFloat("sfxVolume",volume);
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

}
