using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class MoveState :IState
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
    private Transform _playerTransform, _myTransform;


    #endregion
    #endregion

    public MoveState(NavMeshAgent agent, Transform playerTransform, Transform myTransform)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
    }

    public void OnEnterState()
    {
        _navmeshAgent.isStopped = false;
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

    private void NavMeshMove(Transform target)
    {
        _navmeshAgent.destination = target.position;
    }
}
