using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectVolume : MonoBehaviour
{
    Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void EFFECTVolume()
    {
        SoundManager.instance.EffectSoundVolume(slider.value);
    }
}
