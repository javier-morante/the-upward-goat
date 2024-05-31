using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CheckPoint : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.CompareTag("Player")){
            Debug.Log("Player ckeckpoint on "+gameObject.name);       
            DataPersistanceManager.instance.SaveGameData();
        }
    }
}
