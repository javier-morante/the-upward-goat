using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryScreen : MonoBehaviour
{
    void Start(){
        Invoke("ToGame",3);
    }

    void ToGame(){
        TransitionManager.instance.LoadScene(SceneName.Game);
    }
}
