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
    private float _attackDelay = 0f;
    private Conditions _conditions;
    //private float _remainTime;


    #endregion
    #endregion

    public AttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, EnemyInternalSignals enemyInternalSignals, Conditions conditions)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        EnemyInternalSignals = enemyInternalSignals;
        _conditions = conditions;
    }

    public void OnEnterState()
    {
        Debug.Log("enter attack");

        _isAttacking = true;
        if (_navmeshAgent.isActiveAndEnabled)
        {
            _navmeshAgent.isStopped = true;
        }
    }

    public void OnExitState()
    {
        _isAttacking = false;
        Debug.Log("exit attack");

    }

    public void Tick()
    {
        AttackDelay();
        if (_attackDelay > 1.5f)
        {
            ManuelRotation();
        }
    }

    private void ManuelRotation()
    {
        if (_navmeshAgent.isStopped)
        {
            _myTransform.LookAt(_playerTransform);
        }
    }

    private void AttackDelay()
    {
        _attackDelay -= Time.deltaTime;

        if (_isAttacking == false)
        {
            return;
        }

        if (_attackDelay <= 0)
        {
            EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);
            _attackDelay = 1.8f;
        }
    }

    public void OnReset()
    {
        _attackDelay = 0f;
        _isAttacking = false;
    }

    public float TimeDelayToExit()
    {
        _isAttacking = false;
        //_remainTime = _attackDelay;
        //_attackDelay = 0;
        Debug.Log("Time to exit:" + _attackDelay);
        return _attackDelay;
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }
}
