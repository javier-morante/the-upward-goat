using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource soundFXObject2;

    void Awake()
    {
        if(instance == null){
            instance = this;
        }else{

        }
        soundFXObject2 = GetComponent<AudioSource>();
        
    }

    public void PlaySoundFX(AudioClip audioClip,Transform spawn,float volume){

        AudioSource audio  = Instantiate(soundFXObject,spawn.position,Quaternion.identity);

        audio.clip = audioClip;

        audio.volume = volume;

        audio.Play();

        float clipLength = audio.clip.length;

        Destroy(audio.gameObject,clipLength);

    }

    // public void PlaySoundFX(AudioClip audioClip,Transform spawn,float volume){
        
    //     soundFXObject2.clip = audioClip;

    //     soundFXObject2.volume = volume;

    //     soundFXObject2.Play();

    // }
    

    internal void PlaySoundFX(object audioClip, Transform transform, float v)
    {
        throw new NotImplementedException();
    }
}

