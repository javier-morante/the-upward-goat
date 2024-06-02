using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour, IDataPersistence<SettingsData>
{
    // Reference to the text displaying the current option
    [SerializeField] private TextMeshProUGUI optionsText;
    // Reference to the audio mixer
    [SerializeField] private AudioMixer audioMixer;
    // Reference to the main volume slider
    [SerializeField] private Slider mainVolume;
    // Reference to the environment volume slider
    [SerializeField] private Slider enviromentVolume;
    // Reference to the sound effects volume slider
    [SerializeField] private Slider sfxVolume;

    [SerializeField] private Slider jumpslide;
    [SerializeField] private Toggle toggleSlide;
    
    // Array containing the options for full screen and windowed mode
    private string[] options = {"Full Screen", "Window"};
    // Index of the currently selected option
    private int currentOption;

    // Set up initial settings
    void Start(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = (int) Screen.currentResolution.refreshRateRatio.value;
    }

    // Update the settings based on user input
    void Update(){
        OptionExec();
        audioMixer.SetFloat("masterVolume", mainVolume.value - 80);
        audioMixer.SetFloat("enviromentVolume", enviromentVolume.value - 80);
        audioMixer.SetFloat("sfxVolume", sfxVolume.value - 80);
        if(jumpslide != null)jumpslide.gameObject.SetActive(toggleSlide.isOn);
    }

    // Switch to the next option
    public void Next(){
        currentOption += 1;
        if(currentOption >= options.Length){
            currentOption = 0;
        }
    }

    // Switch to the previous option
    public void Previous(){
        currentOption -= 1;
        if(currentOption < 0){
            currentOption = options.Length - 1;
        }
    }

    // Set the main volume level
    public void SetMainVolume(float volume){
        audioMixer.SetFloat("masterVolume", volume - 80);
    }  

    // Set the environment volume level
    public void SetEnviromentVolume(float volume){
        audioMixer.SetFloat("enviromentVolume", volume - 80);
    }  

    // Set the sound effects volume level
    public void SetSFXVolume(float volume){
        audioMixer.SetFloat("sfxVolume", volume - 80);
    }  

    // Execute the selected option
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

    // Load settings data
    public void LoadData(SettingsData data)
    {
        this.mainVolume.value = data.mainVolume;
        this.enviromentVolume.value = data.enviromentVolume;
        this.sfxVolume.value = data.enviromentVolume;
        this.currentOption = data.screenOption;
        this.toggleSlide.isOn = data.jumpChargeBar;
    }

    // Save settings data
    public void SaveData(ref SettingsData data)
    {
        data.mainVolume = this.mainVolume.value;
        data.enviromentVolume = this.enviromentVolume.value;
        data.sfxVolume = this.sfxVolume.value;
        data.screenOption = currentOption;
        data.jumpChargeBar = toggleSlide.isOn;
    }

    public void JumpChargeBar(bool isDisplay){
        if(jumpslide != null)jumpslide.gameObject.SetActive(isDisplay);
    }

    public void Exit(){
        DataPersistanceManager.instance.SaveSettings();
    }
}


