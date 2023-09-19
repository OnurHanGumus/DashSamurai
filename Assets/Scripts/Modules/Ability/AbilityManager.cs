using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityManager : IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] private AbilitySignals _abilitySignals { get; set; }
    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private IAbility[] _abilities;

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
    }
    #endregion

    private void OnActivated(CollectableEnums collectableEnum)
    {
        _abilities[(int)collectableEnum].Activated();
    }

    public void Initialize()
    {
        SubscribeEvents();
    }
}
