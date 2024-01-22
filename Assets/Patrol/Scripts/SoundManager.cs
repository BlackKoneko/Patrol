using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class SoundManager : SingleTon<SoundManager>
{
    public AudioMixer mixer;
    public ObjectPool objectPool;
    
    private void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    public void BgmSoundVolume(float val)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(val) * 20);
    }

    public void EffectSoundVolume(float val)
    {
        mixer.SetFloat("EffectSound", Mathf.Log10(val) * 20);
    }

    public void Play(AudioClip clip, Transform target,bool loopAudio)
    {
        GameObject popObj = objectPool.Pop();
        if(popObj.TryGetComponent(out SoundComponent temp))
        {
            if (loopAudio)
                temp.audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
            else
                temp.audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Effect")[0];
            temp.transform.parent = target;
            temp.Play(clip);
            temp.audioSource.loop = loopAudio;
        }
    }
}
