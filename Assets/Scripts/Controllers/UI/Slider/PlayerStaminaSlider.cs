using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;
using System;
using System.Threading.Tasks;
using DG.Tweening;

public class PlayerStaminaSlider : SliderBase
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals _playerSignals { get; set; }
    [Inject] private SliderIncreaseAutomatically _sliderIncreaseAuomatically;
    [Inject] private AbilitySettings _abilitySettings { get; set; }
    [Inject(Id = "StaminaFill")] private Image _staminaFillImage;

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
        _playerSignals.onUseAbility += IncreaseValue;
        _playerSignals.onIncreaseSkill += IncreaseValue;
        _playerSignals.onGetStaminaValue += GetValue;
        _playerSignals.onLowStamina += OnLowStamina;
    }

    private void UnsubscribeSignals()
    {
        _playerSignals.onUseAbility -= IncreaseValue;
        _playerSignals.onIncreaseSkill -= IncreaseValue;
        _playerSignals.onGetStaminaValue -= GetValue;
        _playerSignals.onLowStamina -= OnLowStamina;
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
        _sliderIncreaseAuomatically.IncreaseValue(slider, _maksValue);
    }

    public override void SetValue(int value)
    {
        base.SetValue(value);
    }

    public override void IncreaseValue(int value)
    {
        base.IncreaseValue(value * (_abilitySettings.AbilityDatas[(int)CollectableEnums.EndlessStamina].IsActivated ? 0 : 1));
        _sliderIncreaseAuomatically.IncreaseValue(slider, _maksValue);
    }

    public override int GetValue()
    {
        return (int) slider.value;
    }

    private void OnLowStamina()
    {
        _staminaFillImage.DOColor(new Color(1, 0, 0), 0.5f).OnComplete(() =>
           {
               _staminaFillImage.DOColor(new Color(0, 229f/255f, 1), 0.5f);
           });
    }
}
