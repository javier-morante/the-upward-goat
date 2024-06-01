using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private ScenesManager scenesManager;
    [SerializeField] private AudioSource[] audioSources;

    void Start(){
        Time.timeScale = 1;
        CursorManager.HideCursor(false);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Pause(); 
        }
    }

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

    public void Resume(){
        pauseMenu.SetActive(false);
        CursorManager.HideCursor(true);
        Time.timeScale = 1;
        foreach (AudioSource audio in audioSources)
        {
            audio.Play();
        }
    }

    public void GiveUp(){
        DataPersistanceManager.instance.DeleteGameData();
        scenesManager.NextSceneWithOutSave("GiveUp");
    }
}
