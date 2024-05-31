using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private ScenesManager scenesManager;

    void Start(){
        if(!DataPersistanceManager.instance.HasGameData()){
            continueButton.interactable = false;
        }
        CursorManager.HideCursor(false);
    }

    public void NewGameButton(){
        DataPersistanceManager.instance.NewGame();
        scenesManager.NextScene("Game");
    }

    public void ExitButton(){
        Application.Quit();
    }
}
