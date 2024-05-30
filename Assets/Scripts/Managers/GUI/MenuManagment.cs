using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    void Start(){
        if(!DataPersistanceManager.instance.HasGameData()){
            continueButton.interactable = false;
        }
    }

    public void NewGameButton(){
        DataPersistanceManager.instance.NewGame();
        SceneManager.LoadScene("Game");
    }

    public void ContinueButton(){
        SceneManager.LoadScene("Game");
    }
    public void ExitButton(){
        Application.Quit();
    }
}
