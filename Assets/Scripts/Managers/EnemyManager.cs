using Components.Players;
using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyManager : MonoBehaviour
{
    #region Self Variables

    #region Inject Variables
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
    #endregion

    #region Serialized Variables
    [SerializeField] private EnemyAnimationController animationController;
    [SerializeField] private EnemyMovementController movementController;

    #endregion
    #region Private Variables

    #endregion
    #endregion


    [Inject] 
    public void Construct(PoolSignals poolSignals, CoreGameSignals coreGameSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
    }

    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation += animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath += movementController.OnDeath;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation -= animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath -= movementController.OnDeath;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
}
