using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
   [SerializeField] private bool onStart;
   
   [TextArea(3,10)]
   [SerializeField] private string[] sentences;

   void Start(){
      if(onStart)
      {
         FindObjectOfType<DialogManager>().StartDialog(sentences);
      }
   }

   public void TriggerDialoge(){
        FindObjectOfType<DialogManager>().StartDialog(sentences);
   }
}
