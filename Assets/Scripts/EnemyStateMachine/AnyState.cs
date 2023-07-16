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


    #endregion
    #endregion

    public AnyState(NavMeshAgent agent)
    {
        _navmeshAgent = agent;
    }

    public void OnEnterState()
    {
        Stop();
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

    private void Stop()
    {
        _navmeshAgent.isStopped = true;
    }
}
