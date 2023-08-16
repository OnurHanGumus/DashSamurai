using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class DeadState :IState
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

    #endregion
    #endregion

    public DeadState(NavMeshAgent agent)
    {
        _navmeshAgent = agent;
    }

    public bool IsItReadyToExit()
    {
        return true;
    }

    public void OnEnterState()
    {
        StopMovement();
    }

    public void OnExitState()
    {

    }

    public void OnReset()
    {

    }

    public void Tick()
    {

    }

    public void ConditionCheck()
    {
        
    }

    public float TimeDelayToExit()
    {
        return 0;
    }

    private void StopMovement()
    {
        if(_navmeshAgent.isActiveAndEnabled)
            _navmeshAgent.isStopped = true;
    }
}
