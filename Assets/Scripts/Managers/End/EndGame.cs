using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Reference to the animator component
    [SerializeField] private Animator animator;
    // Flag to track if the end game trigger has been activated for the first time
    private bool firstTime = true;

    // Called when another collider enters the trigger zone
    void OnTriggerEnter2D(Collider2D collider2D){
        // If it's the first time the trigger is activated
        if(firstTime){
            // Trigger the "Open" animation in the animator
            animator.SetTrigger("Open");
            // Set the firstTime flag to false to prevent further triggers
            firstTime = false;
        }
   }

    // Method to handle the end of the game
    public void End(){
        // Call the TransitionManager to load the end scene
        TransitionManager.instance.LoadScene(SceneName.End);
    }
}
