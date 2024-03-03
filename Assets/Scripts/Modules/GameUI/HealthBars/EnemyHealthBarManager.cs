using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyHealthBarManager : HealthBarManager
{
    #region Self Variables
    #region Inject Variables
    [Inject] private EnemyInternalSignals _enemyInternalSignals { get; set; }
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
        //_enemyInternalSignals.onHitted += SetHealthBarScale;
    }

    private void UnSubscribeEvent()
    {
        //_enemyInternalSignals.onHitted -= SetHealthBarScale;
    }

    private void OnDisable()
    {
        UnSubscribeEvent();
    }

    #endregion
}
