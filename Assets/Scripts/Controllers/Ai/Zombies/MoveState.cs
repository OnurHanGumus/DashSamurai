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

    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables
    private EnemyManager2 _manager;
    private EnemyAnimationController _animationController;
    private NavMeshAgent _navmeshAgent;
    private Transform _playerTransform;
    private EnemyInternalSignals _enemyInternalSignals;
    private Conditions _conditions;
    private EnemySettings _mySettings;


    #endregion
    #endregion

    public MoveState(NavMeshAgent agent, EnemyManager2 manager, Transform playerTransform, Conditions conditions, EnemyAnimationController animationControler, EnemySettings enemySettings)
    {
        _navmeshAgent = agent;
        _manager = manager;
        _playerTransform = playerTransform;
        _conditions = conditions;
        _animationController = animationControler;
        _mySettings = enemySettings;
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
