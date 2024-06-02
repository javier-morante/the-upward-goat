using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool firstTime = true;

    void OnTriggerEnter2D(Collider2D collider2D){
        if(firstTime){
            animator.SetTrigger("Open");
            firstTime = false;
        }
   }

    public void End(){
        TransitionManager.instance.LoadScene(SceneName.End);
    }

}
