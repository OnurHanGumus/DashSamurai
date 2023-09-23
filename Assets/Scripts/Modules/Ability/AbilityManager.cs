using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityManager : IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] private AbilitySignals _abilitySignals { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private IAbility[] _abilities;
    private IAbility _currentAbility;

    #endregion
    #endregion

    [Inject]
    public AbilityManager(params IAbility[] abilities)
    {
        _abilities = abilities;
    }

    #region Event Subscriptions
    private void SubscribeEvents()
    {
        _abilitySignals.onActivateAbility += OnActivated;
        _coreGameSignals.onRestart += OnDeactivated;
    }
    #endregion

    private void OnActivated(CollectableEnums collectableEnum)
    {
        _currentAbility = _abilities[(int)collectableEnum];

        _currentAbility.Activated();
        _abilitySignals.onAbilityNameChanged?.Invoke(_currentAbility.GetName());
    }

    private void OnDeactivated()
    {
        if (_currentAbility == null)
        {
            return;
        }
        _currentAbility.Deactivated();
    }

    public void Initialize()
    {
        SubscribeEvents();
    }
}
