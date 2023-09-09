using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;
using System.Threading.Tasks;

public class PlayerStaminaSlider : SliderBase
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private SliderIncreaseAutomatically SliderIncreaseAuomatically;

    #endregion
    #region Public Variables
    #endregion
    #region SerializeField Variables

    #endregion
    #region Private Variables
    private bool _isIncreasing = false;

    #endregion
    #endregion

    #region Event Subscribtion
    private void OnEnable()
    {
        SubscribeSignals();
    }

    private void SubscribeSignals()
    {
        PlayerSignals.onUseAbility += IncreaseValue;
        PlayerSignals.onIncreaseSkill += IncreaseValue;
        PlayerSignals.onGetStaminaValue += GetValue;
    }

    private void UnsubscribeSignals()
    {
        PlayerSignals.onUseAbility -= IncreaseValue;
        PlayerSignals.onIncreaseSkill -= IncreaseValue;
        PlayerSignals.onGetStaminaValue -= GetValue;
    }

    private void OnDisable()
    {
        UnsubscribeSignals();
    }
    #endregion


    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        SliderIncreaseAuomatically.IncreaseValue(slider, _maksValue);

    }

    public override void SetValue(int value)
    {
        base.SetValue(value);
    }

    public override void IncreaseValue(int value)
    {
        base.IncreaseValue(value);
        SliderIncreaseAuomatically.IncreaseValue(slider, _maksValue);
    }

    public override int GetValue()
    {
        return (int) slider.value;
    }
}
