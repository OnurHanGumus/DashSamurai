using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SliderBase : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    [Inject] protected Slider slider;
    [Inject] protected TextMeshProUGUI sliderText;
    #endregion
    #region Public Variables

    #endregion
    #region SerializeField Variables

    #endregion
    #region Private Variables

    #endregion
    #region Protected Variables
    protected int _maksValue = 100;

    #endregion
    #endregion
    public virtual void Init()
    {
        slider.onValueChanged.AddListener(delegate { UpdateSliderText(slider.value); });
    }

    public virtual void SetValue(int value)
    {
        slider.value = value;
    }

    public virtual void IncreaseValue(int value)
    {
        slider.value += value;
    }

    public virtual void UpdateSliderText(float value)
    {
        sliderText.text = ((int)value).ToString();
    }

    public virtual void SetMaksimumValue(int value)
    {
        slider.maxValue = value;
    }

    public virtual int GetValue()
    {
        return (int) slider.value;
    }
}
