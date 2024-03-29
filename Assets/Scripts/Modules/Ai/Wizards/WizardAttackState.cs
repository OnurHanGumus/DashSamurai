using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Enums;
using System;
using Signals;
using System.Threading;
using Components.Enemies;

public class WizardAttackState : IState, IInitializable
{
    #region Self Variables

    #region Inject Variables
    [Inject] private AudioSignals _audioSignals { get; set; }
    [Inject] private NavMeshAgent _navmeshAgent;
    [Inject] private EnemySettings _settings;
    [Inject] private EnemyAnimationController _animationController;
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private EnemyInternalSignals _enemyInternalSignals;
    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables
    private Transform _playerTransform, _myTransform;
    private bool _isAttacking = false, _isBlocked = false;
    private float _attackDelay = 0f;
    private Conditions _conditions;
    private Transform _mageInitTransform;
    private GameObject magic;
    private CancellationTokenSource cancellationToken;

    #endregion
    #endregion

    public WizardAttackState(Transform playerTransform, Transform myTransform, Conditions conditions, Transform mageInitPos)
    {
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _conditions = conditions;
        _mageInitTransform = mageInitPos;
    }

    private void SubscribleEvents()
    {
        _enemyInternalSignals.onDisabled += OnDeath;
    }

    public void Initialize()
    {
        SubscribleEvents();
    }

    public void OnEnterState()
    {
        _isBlocked = false;

        _attackDelay = _settings.AttackDelay;

        _animationController.ResetTrigger(EnemyAnimationStates.Move);
        _animationController.ChangeAnimation(EnemyAnimationStates.Attack1);
        cancellationToken = new CancellationTokenSource();
        ThrowMage(0.8f, cancellationToken.Token);
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
        cancellationToken.Cancel();
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

            ThrowMage(0.8f, cancellationToken.Token);
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

    private async Task ThrowMage(float delay, CancellationToken cancellationToken)
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay/10));
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }
        }

        if (_isBlocked)
        {
            return;
        }
        _audioSignals.onPlaySound?.Invoke(AudioSoundEnums.ThrowMage);
        magic = PoolSignals.onGetObjectExpanded?.Invoke(PoolEnums.WizardMage, _mageInitTransform.position, Quaternion.LookRotation((_playerTransform.position - _myTransform.position).normalized));
        magic.SetActive(true);
    }

    private void OnDeath()
    {
        _isBlocked = true;
    }
}
