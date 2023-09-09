using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Enums;
using Data.MetaData;
using System;
using Signals;
using System.Threading;
using Components.Enemies;

public class WizardAttackState : IState
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
    private bool _isAttacking = false, _isBlocked = false;
    private float _attackDelay = 0f;
    private Conditions _conditions;
    private EnemySettings _settings;
    private EnemyAnimationController _animationController;
    private Transform _mageInitTransform;
    private PoolSignals PoolSignals { get; set; }
    private EnemyInternalSignals _enemyInternalSignals;

    GameObject magic;


    #endregion
    #endregion

    public WizardAttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, Conditions conditions, EnemySettings settings, EnemyAnimationController animationController, PoolSignals poolSignals, Transform mageInitPos, EnemyInternalSignals enemyInternalSignals)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _conditions = conditions;
        _settings = settings;
        _animationController = animationController;
        PoolSignals = poolSignals;
        _mageInitTransform = mageInitPos;
        _enemyInternalSignals = enemyInternalSignals;
        SubscribleEvents();
    }

    private void SubscribleEvents()
    {
        _enemyInternalSignals.onDisabled += OnDeath;
    }

    public void OnEnterState()
    {
        _isBlocked = false;

        _attackDelay = _settings.AttackDelay;

        _animationController.ResetTrigger(EnemyAnimationStates.Move);
        _animationController.ChangeAnimation(EnemyAnimationStates.Attack1);
        ThrowMage(0.8f);
        //StartAsyncMethod();


        _isAttacking = true;
        if (_navmeshAgent.isActiveAndEnabled)
        {
            _navmeshAgent.isStopped = true;
        }
    }

    public void OnExitState()
    {
        _isAttacking = false;
        _isBlocked = true;
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
            _myTransform.LookAt(new Vector3(_playerTransform.position.x, _myTransform.position.y, _playerTransform.position.z));
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
            _animationController.ChangeAnimation(EnemyAnimationStates.Attack1);
            _attackDelay = _settings.AttackDelay;

            ThrowMage(0.8f);
            //StartAsyncMethod();
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

    private async Task ThrowMage(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        if (_isBlocked)
        {
            return;
        }
        magic = PoolSignals.onGetObjectExpanded?.Invoke(PoolEnums.WizardMage, _mageInitTransform.position, Quaternion.LookRotation((_playerTransform.position - _myTransform.position).normalized));
        magic.SetActive(true);
    }

    private void OnDeath()
    {
        _isBlocked = true;
    }
}
