using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource soundFXObject;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }else{

        }
        
    }

    public void PlaySoundFX(AudioClip audioClip,Transform spawn,float volume){

        AudioSource audio  = Instantiate(soundFXObject,spawn.position,Quaternion.identity);

        audio.clip = audioClip;

        audio.volume = volume;

        audio.Play();

        float clipLength = audio.clip.length;

        Destroy(audio.gameObject,clipLength);

    }

}
