using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeGame : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("SliderVolume").GetComponent<Slider>().value = GameObject.Find("ParamStart").GetComponent<Parameter>().Volume*100;
    }
    public void ChangeVolume()
    {
        GameObject.Find("ParamStart").GetComponent<Parameter>().Volume = GameObject.Find("SliderVolume").GetComponent<Slider>().value / 100;
    }
}
