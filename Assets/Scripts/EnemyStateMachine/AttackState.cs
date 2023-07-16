using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class AttackState :IState
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
    private EnemyInternalSignals EnemyInternalSignals { get; set; }
    private bool _isAttacking = false;


    #endregion
    #endregion

    public AttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, EnemyInternalSignals enemyInternalSignals)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        EnemyInternalSignals = enemyInternalSignals;
    }

    public void OnEnterState()
    {
        _isAttacking = true;
        _navmeshAgent.isStopped = true;
        AttackAnimationDelay();
    }

    public void OnExitState()
    {
        _isAttacking = false;

    }

    public void Tick()
    {
        ManuelRotation();
    }

    private void ManuelRotation()
    {
        if (_navmeshAgent.isStopped)
        {
            _myTransform.LookAt(_playerTransform);
        }
    }

    private async Task AttackAnimationDelay()
    {
        while (_isAttacking)
        {
            EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);
            await Task.Delay(System.TimeSpan.FromSeconds(2.3f));
        }
    }

    public void OnReset()
    {
        _isAttacking = false;
    }
}
