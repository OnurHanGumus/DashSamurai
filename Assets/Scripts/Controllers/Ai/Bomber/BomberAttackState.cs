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
using Controllers;

public class BomberAttackState : IState
{
    #region Self Variables

    #region Inject Variables
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }

    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables
    private NavMeshAgent _navmeshAgent;
    private Transform _playerTransform, _myTransform;
    private bool _isAttacking = false, _isBlocked = false;
    private Conditions _conditions;
    private EnemySettings _settings;
    private EnemyAnimationController _animationController;
    private GameObject magic;
    private IAttackable _physicsController;
    #endregion
    #endregion

    public BomberAttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, Conditions conditions, EnemySettings settings, EnemyAnimationController animationController, IAttackable attackable)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _conditions = conditions;
        _settings = settings;
        _animationController = animationController;
        _physicsController = attackable;
    }

    public void OnEnterState()
    {
        _isBlocked = false;

        _animationController.ResetTrigger(EnemyAnimationStates.Move);
        _animationController.ChangeAnimation(EnemyAnimationStates.Attack1);

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
        ManuelRotation();
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
        if (_isAttacking == false)
        {
            return;
        }

        _isAttacking = false;
        Explode(0.5f);
    }

    public void OnReset()
    {
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

    private async Task Explode(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        if (_isBlocked)
        {
            return;
        }

        magic = PoolSignals.onGetObject?.Invoke(PoolEnums.BomberExplode, _myTransform.position);
        magic.SetActive(true);
        //await Task.Delay(TimeSpan.FromSeconds(_settings.DeathDuration));
        _physicsController.OnWeaponTriggerEnter(500);

    }
}
