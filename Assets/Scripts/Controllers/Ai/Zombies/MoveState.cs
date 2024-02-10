using Data.MetaData;
using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MoveState : IState
{
    #region Self Variables

    #region Inject Variables
    [Inject] private EnemyManager2 _manager;
    [Inject] private EnemyAnimationController _animationController;
    [Inject] private NavMeshAgent _navmeshAgent;
    [Inject] private EnemyInternalSignals _enemyInternalSignals;
    [Inject] private EnemySettings _mySettings;

    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables
    
    private Transform _playerTransform;
    private Conditions _conditions;


    #endregion
    #endregion

    public MoveState(Transform playerTransform, Conditions conditions)
    {
        _playerTransform = playerTransform;
        _conditions = conditions;
    }

    public bool IsItReadyToExit()
    {
        return true;
    }

    public void OnEnterState()
    {
        if (_navmeshAgent.isActiveAndEnabled)
        {
            _navmeshAgent.isStopped = false;
        }

        SetAnimations();
    }

    private void SetAnimations()
    {
        _animationController.ResetTrigger(EnemyAnimationStates.Attack1);
        _animationController.ResetTrigger(EnemyAnimationStates.Move);
        _animationController.ChangeAnimation(EnemyAnimationStates.Move);
    }

    public void OnExitState()
    {

    }

    public void OnReset()
    {

    }

    public void Tick()
    {
        NavMeshMove(_playerTransform);
    }

    public float TimeDelayToExit()
    {
        return 0f;
    }

    private void NavMeshMove(Transform target)
    {
        _navmeshAgent.destination = target.position;
        _navmeshAgent.speed = _mySettings.MoveSpeed * _manager.speedPercent;
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }
}
