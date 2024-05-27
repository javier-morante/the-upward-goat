using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private AudioSource[] audioSources;

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape)){
            Pause(); 
        }
    }

    public void Pause(){
        pauseMenu.SetActive(true);
        optionMenu.SetActive(false);
        Time.timeScale = 0;
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }
    }

    public void Resume(){
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        foreach (AudioSource audio in audioSources)
        {
            audio.Play();
        }

    }

    public void Options(){
        pauseMenu.SetActive(false);
        optionMenu.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }
}
