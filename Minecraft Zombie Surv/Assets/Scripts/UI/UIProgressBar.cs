using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UIProgressBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float maxValue)
    {
        if (!slider) slider = GetComponent<Slider>();

        slider.maxValue = maxValue;
        slider.value = maxValue;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
