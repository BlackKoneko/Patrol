using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundComponent : MonoBehaviour
{
    public AudioSource audioSource;


    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void Update()
    {
        if (audioSource.isPlaying == false)
        {
            SoundManager.instance.ReturnPool(gameObject);
        }
    }
}
