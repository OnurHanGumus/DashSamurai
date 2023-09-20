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

public class BomberAttackState : IState
{
    #region Self Variables

    #region Inject Variables
    [Inject] private LevelSignals LevelSignals { get; set; }
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
    [Inject] private PoolSignals PoolSignals { get; set; }
    GameObject magic;

    #endregion
    #endregion

    public BomberAttackState(NavMeshAgent agent, Transform playerTransform, Transform myTransform, Conditions conditions, EnemySettings settings, EnemyAnimationController animationController)
    {
        _navmeshAgent = agent;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _conditions = conditions;
        _settings = settings;
        _animationController = animationController;
    }

    public void OnEnterState()
    {
        _isBlocked = false;

        _attackDelay = _settings.AttackDelay;

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
        if (_isAttacking == false)
        {
            return;
        }

        _isAttacking = false;
        Explode(_attackDelay);
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

    private async Task Explode(float delay)
    {
        await Task.Delay(TimeSpan.FromSeconds(delay));
        if (_isBlocked)
        {
            return;
        }

        magic = PoolSignals.onGetObject?.Invoke(PoolEnums.BomberExplode, _myTransform.position);
        magic.SetActive(true);
        LevelSignals.onEnemyDied?.Invoke();
        //await Task.Delay(TimeSpan.FromSeconds(_settings.DeathDuration));

        _myTransform.gameObject.SetActive(false);

    }
}
