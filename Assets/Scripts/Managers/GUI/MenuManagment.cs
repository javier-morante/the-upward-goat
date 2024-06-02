using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManagment : MonoBehaviour
{
    // Reference to the continue button
    [SerializeField] private Button continueButton;

    void Start(){
        // Reset the time scale to normal
        Time.timeScale = 1;
        // Show the cursor
        CursorManager.HideCursor(false);

        // Check if there is saved game data
        continueButton.interactable = DataPersistanceManager.instance.HasGameData();
    }

    // Method to start a new game
    public void NewGameButton(){
        // Start a new game
        DataPersistanceManager.instance.NewGame();
        // Save the settings
        DataPersistanceManager.instance.SaveSettings();
        // Transition to the history explanation scene with a progress bar
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.HistoryExplain);
    }

    // Method to continue the game from the last saved state
    public void Continue(){
        // Transition to the game scene with a progress bar
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Game);
    }

    // Method to quit the application
    public void ExitButton(){
        // Quit the application
        Application.Quit();
    }
}

