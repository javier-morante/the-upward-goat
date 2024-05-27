using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject optionMenu;
        [SerializeField] TextMeshProUGUI optionsText;
        private string[] options = {"Full Screen","Window"};
        [SerializeField]private int currentOption;
        [SerializeField] private AudioMixer audioMixer;

        void Start(){
            currentOption = 0;
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
        public void Previus(){
            currentOption -=1;
            if(currentOption < 0){
                currentOption = options.Length-1;
            }
        }

        public void Back(){
            pauseMenu.SetActive(true);
            optionMenu.SetActive(false);
        }

        public void SetVolume(float volume){
            audioMixer.SetFloat("volume",volume);
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
