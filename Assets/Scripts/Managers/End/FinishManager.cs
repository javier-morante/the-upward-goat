using UnityEngine;

public class FinishManager : MonoBehaviour
{
    void Start()
    {
        // Reset the time scale to normal
        Time.timeScale = 1;
        // Hide the cursor
        CursorManager.HideCursor(true);

        // Check if there is saved game data
        if (DataPersistanceManager.instance.HasGameData())
        {
            // Retrieve the saved game data
            GameData gameData = DataPersistanceManager.instance.GetGameData();

            // Prepare the sentences for the dialog manager
            string[] sentences = {
                "Game Time: "+UiManager.FormatStringToTime(gameData.cronometer),
                "Jumps made: "+gameData.jumpCount,
                "Coins collected: "+gameData.coinsCollected
            };

            // Start the dialog with the prepared sentences
            FindObjectOfType<DialogManager>().StartDialog(sentences);
        }
    }

    // Method to transition to the main menu
    public void ToMenu(){
        // Load the main menu scene with a progress bar transition
        TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Menu);
    }
}
