using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUpManager : MonoBehaviour
{
    [SerializeField] private ScenesManager scenesManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float secondsBetween;
    [SerializeField] private float time;
    void Update()
    {
        time += Time.deltaTime;
        if (time > secondsBetween)
        {
            audioSource.Play();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            scenesManager.NextSceneWithOutSave("Menu");
        }
    }
}
