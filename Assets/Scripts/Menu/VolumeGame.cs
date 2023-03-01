using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeGame : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value = FindObjectOfType<Parameter>().soundVolume*100;
        GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value = FindObjectOfType<Parameter>().musicVolume*100;
    }
    public void ChangeSoundVolume(float value)
    {
        FindObjectOfType<Parameter>().soundVolume = value / 100;
    }
    public void ChangeMusicVolume(float value)
    {
        FindObjectOfType<Parameter>().musicVolume = value / 100;
    }
}
