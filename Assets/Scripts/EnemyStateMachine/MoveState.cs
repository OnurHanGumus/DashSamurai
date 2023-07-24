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
    private NavMeshAgent _navmeshAgent;
    private Transform _playerTransform;
    private EnemyInternalSignals _enemyInternalSignals;
    private Conditions _conditions;


    #endregion
    #endregion

    public MoveState(NavMeshAgent agent, Transform playerTransform, Conditions conditions, EnemyInternalSignals enemyInternalSignals)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _conditions = conditions;
        _enemyInternalSignals = enemyInternalSignals;
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

        _enemyInternalSignals.onChangeAnimation?.Invoke(Enums.EnemyAnimationStates.Move);
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
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }
}
