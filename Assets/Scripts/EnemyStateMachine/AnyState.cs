using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AnyState :IState
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
    private Conditions _conditions;
    private float _exitDelay = 0f;

    #endregion
    #endregion

    public AnyState(NavMeshAgent agent, Conditions conditions)
    {
        _navmeshAgent = agent;
        _conditions = conditions;
    }

    public bool IsItReadyToExit()
    {
        return true;
    }

    public void OnEnterState()
    {
        Debug.Log("enter any");

        StopMovement();
        _exitDelay = 0.8f;
    }

    public void OnExitState()
    {
        Debug.Log("exit any");

        _exitDelay = 0f;
    }

    public void OnReset()
    {
        _exitDelay = 0f;
    }

    public void Tick()
    {
        Timer();
    }

    private void Timer()
    {
        _exitDelay -= Time.deltaTime;
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }

    public float TimeDelayToExit()
    {
        return _exitDelay;
    }

    private void StopMovement()
    {
        if(_navmeshAgent.isActiveAndEnabled)
            _navmeshAgent.isStopped = true;
    }
}
