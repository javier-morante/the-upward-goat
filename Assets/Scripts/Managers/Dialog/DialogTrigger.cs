using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
   // Flag indicating whether the dialogue should start automatically when the object is enabled
   [SerializeField] private bool onStart;
   
   // Array of sentences for the dialogue
   [TextArea(3,10)]
   [SerializeField] private string[] sentences;

   void Start(){
      // If onStart is true, start the dialogue automatically when the object is enabled
      if(onStart)
      {
         FindObjectOfType<DialogManager>().StartDialog(sentences);
      }
   }

   // Method to manually trigger the dialogue
   public void TriggerDialoge(){
        FindObjectOfType<DialogManager>().StartDialog(sentences);
   }
}
