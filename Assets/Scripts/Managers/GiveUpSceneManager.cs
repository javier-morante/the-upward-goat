using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUpManage : MonoBehaviour
{ 
    // Reference to the audio source for playing sounds
    [SerializeField] private AudioSource audioSource;
    // Time interval between playing sounds
    [SerializeField] private float secondsBetweenSound;

    // Timer to track time intervals
    private float time;

    void Update()
    {
        // Hide the cursor
        CursorManager.HideCursor(true);
        
        // Increment the timer
        time += Time.deltaTime;

        // Check if it's time to play the sound
        if (time > secondsBetweenSound)
        {
            // Play the audio source
            audioSource.Play();
            // Reset the timer
            time = 0;
        }

        // Check if the Escape key is pressed
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            // Load the menu scene with a progress bar transition
            TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Menu);
        }
    }
}

