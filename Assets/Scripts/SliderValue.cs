using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public TextMeshProUGUI valueTxt;
    public Slider slider;

    private void Start()
    {
        var value = PlayerPrefs.GetInt("duration");
        slider.value = value;
    }
    void Update()
    {
        valueTxt.text = slider.value + " seg";
        PlayerPrefs.SetInt("duration", (int)slider.value);
    }
}
