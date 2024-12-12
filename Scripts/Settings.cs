using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioMixer generalMixer;

    protected virtual void Start()
    {
        LoadVolume();
    }

    public virtual void SetVolume()
    {
        float volume = volumeSlider.value;
        generalMixer.SetFloat("General Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("VolumeSave", volume);
    }

    public virtual void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeSave");
        SetVolume();
    }
}
