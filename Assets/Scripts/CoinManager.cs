using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour,IObserver<PlayerEvents>
{
    private int coinCount;
    private int jumpCounter;
    [SerializeField] private Text coinText;
    [SerializeField] private Text jumpText;
    [SerializeField] private Subject<PlayerEvents> subject;
    public void OnNotify(PlayerEvents notification)
    {
        switch (notification)
        {
            case PlayerEvents.CoinCollected:
                coinCount ++;
                break;
            case PlayerEvents.Jump:
                jumpCounter++;
                break;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        subject.AddObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coins:"+coinCount;
        jumpText.text = "Jumps:"+jumpCounter;
    }
}
