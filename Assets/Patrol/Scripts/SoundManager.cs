using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingleTon<SoundManager>
{
    public GameObject audioClone;
    public AudioMixer mixer;
    [SerializeField] GameObject soundComponentPrafab;
    Queue<GameObject> pool = new Queue<GameObject>();
    

    private void Start()
    {
        Init();
    }

    public void BgmSoundVolume(float val)
    {
        Debug.Log("BGMº¼·ý" + val);
        mixer.SetFloat("BgmSound", Mathf.Log10(val) * 20);
    }

    public void EffectSoundVolume(float val)
    {
        Debug.Log("EFFFº¼·ý" + val);
        mixer.SetFloat("EffectSound", Mathf.Log10(val) * 20);
    }

    void Init()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject temp = Instantiate(soundComponentPrafab, audioClone.transform);
            temp.SetActive(false);
            pool.Enqueue(temp);
        }
    }
    public SoundComponent Pop()
    {
        GameObject popObj = pool.Dequeue();
        popObj.SetActive(true);
        return popObj.GetComponent<SoundComponent>();
    }
    public void ReturnPool(GameObject returnObj)
    {
        returnObj.SetActive(false);
        returnObj.transform.SetParent(audioClone.transform);
        returnObj.GetComponent<SoundComponent>().audioSource.loop = false;
        pool.Enqueue(returnObj);
    }
    public void Play(AudioClip clip, Transform target,bool loopAudio)
    {
        SoundComponent temp = Pop();
        if (loopAudio)
            temp.audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
        else
            temp.audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Effect")[0];
        temp.transform.parent = target;
        temp.Play(clip);
        temp.audioSource.loop = loopAudio;
    }
}
