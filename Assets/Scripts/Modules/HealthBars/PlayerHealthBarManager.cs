using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthBarManager : HealthBarManager
{
    #region Self Variables
    #region Inject Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    #endregion
    #region Public Variables

    #endregion

    #region Serialized Variables

    #endregion

    #region Private Variables

    #endregion

    #endregion
    #region Event Subscription
    private void Start()
    {
        SubscribeEvent();
    }

    protected override void Update()
    {
        base.Update();
    }

    private void SubscribeEvent()
    {
        PlayerSignals.onHitted += SetHealthBarScale;
    }

    private void UnSubscribeEvent()
    {
        PlayerSignals.onHitted -= SetHealthBarScale;
    }

    private void OnDisable()
    {
        UnSubscribeEvent();
    }

    #endregion
}
