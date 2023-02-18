using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderDelay : MonoBehaviour
{
    public TextMeshProUGUI valueTxt;
    public Slider slider;

    float value = 0f;
    private void Start()
    {
        var delay = PlayerPrefs.GetFloat("spawnDelay");
        if (delay == 0.5f) value = 0f;
        else value = (delay - 0.5f) / 0.25f;
        slider.value = value;
    }
    void Update()
    {
        value = 0.5f + (slider.value * 0.25f);
        valueTxt.text = value + " seg";
        PlayerPrefs.SetFloat("spawnDelay", value);
    }
}
