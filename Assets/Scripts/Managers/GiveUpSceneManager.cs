using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUpManage : MonoBehaviour
{ 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float secondsBetweenSound;
    private float time;
    void Update()
    {
        CursorManager.HideCursor(true);
        time += Time.deltaTime;
        if (time > secondsBetweenSound)
        {
            audioSource.Play();
            time = 0;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
           TransitionManager.instance.LoadSceneWithProgressBar(SceneName.Menu);
        }
    }
}
