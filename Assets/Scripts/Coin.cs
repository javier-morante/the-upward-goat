using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : Subject<PlayerEvents>{

    private string id;

    void Start(){

        UiManager UiManager = GameObject.Find("GameManager").GetComponent<UiManager>();
        this.AddObserver(UiManager);
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("Player")){
            this.NotifyObservers(PlayerEvents.CoinCollected);
            gameObject.SetActive(false);
        }
    }
}
