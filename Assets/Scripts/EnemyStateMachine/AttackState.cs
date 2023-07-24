using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Data.MetaData;

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
    private EnemySettings _settings;

    #endregion
    #endregion

    public AttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, EnemyInternalSignals enemyInternalSignals, Conditions conditions, EnemySettings settings)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        EnemyInternalSignals = enemyInternalSignals;
        _conditions = conditions;
        _settings = settings;
    }

    public void OnEnterState()
    {
        _attackDelay = _settings.AttackDelay;
        EnemyInternalSignals.onResetAnimation?.Invoke(Enums.EnemyAnimationStates.Move);

        EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);

        _isAttacking = true;
        if (_navmeshAgent.isActiveAndEnabled)
        {
            _navmeshAgent.isStopped = true;
        }
    }

    public void OnExitState()
    {
        _isAttacking = false;
    }

    public void Tick()
    {
        AttackDelay();
        if (_attackDelay > _settings.AttackRotatableTime)
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

        if (_attackDelay <= 0f)
        {
            EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);
            _attackDelay = _settings.AttackDelay;
        }
    }

    public void OnReset()
    {
        _attackDelay = 0f;
        _isAttacking = false;
    }

    public float TimeDelayToExit()
    {
        return 0;
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }
}
