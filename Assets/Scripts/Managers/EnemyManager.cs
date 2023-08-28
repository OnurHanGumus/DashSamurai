using Components.Players;
using Enums;
using Signals;
using System;
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
    [SerializeField] private Vector3 playerMeshInitPos;
    [SerializeField] private Transform playerMesh;

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
        SetPlayerMeshInitPos();
    }

    private void SetPlayerMeshInitPos()
    {
        playerMesh.position = playerMeshInitPos;
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation += animationController.ChangeAnimation;
        EnemyInternalSignals.onDeath += movementController.OnDeath;
        EnemyInternalSignals.onHitted += movementController.OnHitted;
        EnemyInternalSignals.onAttack += movementController.OnAttack;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation -= animationController.ChangeAnimation;
        EnemyInternalSignals.onDeath -= movementController.OnDeath;
        EnemyInternalSignals.onHitted -= movementController.OnHitted;
        EnemyInternalSignals.onAttack -= movementController.OnAttack;
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
