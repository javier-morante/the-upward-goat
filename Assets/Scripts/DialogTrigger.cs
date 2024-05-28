using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
   [SerializeField] private Dialog dialog;
    private bool firstTime = true;

    public void TriggerDialoge(){
        FindObjectOfType<DialogManager>().StartDialog(dialog);

   }

   void OnTriggerEnter2D(Collider2D collider2D){
        if(firstTime){
            TriggerDialoge();
            firstTime = false;
        }

   }
}
