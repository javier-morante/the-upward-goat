using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : Subject<PlayerEvents>,IDataPersistence<GameData>{

    public string id;
    private bool collected;
    [SerializeField] private AudioClip coin;

    [ContextMenu("Generate id")]
    private void GenerateGui(){
        id = System.Guid.NewGuid().ToString();
    }

    void Start(){

        UiManager UiManager = GameObject.Find("GameManager").GetComponent<UiManager>();
        this.AddObserver(UiManager);
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.CompareTag("Player")){
            this.NotifyObservers(PlayerEvents.CoinCollected);
            AudioManager.instance.PlaySoundFX(coin, transform, 1f);
            gameObject.SetActive(false);
            collected = true;
        }
    }

    public void LoadData(GameData data)
    {

        data.coins.TryGetValue(id,out collected);
        if(collected){
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.coins.ContainsKey(id))
        {
            data.coins.Remove(id);
        }
        data.coins.Add(id,collected);
    }
}
