using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    void Start(){
        Time.timeScale = 1;
        CursorManager.HideCursor(false);

        if(!DataPersistanceManager.instance.HasGameData()){
            continueButton.interactable = false;
        }
        
    }

    public void NewGameButton(){
        DataPersistanceManager.instance.NewGame();
        DataPersistanceManager.instance.SaveSettings();
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.HistoryExplain);
    }

    public void Continue(){
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Game);
    }

    public void ExitButton(){
        Application.Quit();
    }
}
