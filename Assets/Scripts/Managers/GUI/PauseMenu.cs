using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Reference to the pause menu UI
    [SerializeField] private GameObject pauseMenu;
    // Reference to the options menu UI
    [SerializeField] private GameObject optionMenu;
    // Array of audio sources to be paused/resumed
    [SerializeField] private AudioSource[] audioSources;

    // Set up initial settings
    void Start(){
        Time.timeScale = 1;
        CursorManager.HideCursor(true);
    }

    // Check for input to pause the game
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Pause(); 
        }
    }

    // Pause the game
    public void Pause(){
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        CursorManager.HideCursor(false);
        Time.timeScale = 0;
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    // Resume the game
    public void Resume(){
        pauseMenu.SetActive(false);
        CursorManager.HideCursor(true);
        Time.timeScale = 1;
        foreach (AudioSource audio in audioSources)
        {
            audio.Play();
        }
    }

    // Handle the "Give Up" action
    public void GiveUp(){
        DataPersistanceManager.instance.DeleteGameData();
        TransitionManager.instance.LoadScene(SceneName.GiveUp);
    }

    // Exit the game
    public void Exit(){
        DataPersistanceManager.instance.SaveGameData();
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Menu);
    }
}

