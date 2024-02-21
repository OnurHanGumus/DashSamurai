using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class AnyState : IState
{
    #region Self Variables

    #region Inject Variables
    [Inject] private NavMeshAgent _navmeshAgent;
    [Inject] private EnemyAnimationController _animationController;
    [Inject] private EnemySettings _settings;
    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables

    private Conditions _conditions;
    private float _exitDelay = 0f;
    private int _randomHittedAnimId;

    #endregion
    #endregion

    public AnyState(Conditions conditions)
    {
        _conditions = conditions;
    }

    public bool IsItReadyToExit()
    {
        return true;
    }

    public void OnEnterState()
    {
        StopMovement();
        _exitDelay = _settings.AnyStateDuration;

        _animationController.SetSpeed(Random.Range(2f, 2.5f));
        _animationController.SetBlend(Random.Range(0.3f, 0.9f));
        _randomHittedAnimId = Random.Range(0, 1);

        _animationController.ResetTrigger(EnemyAnimationStates.Attack1);
        _animationController.ResetTrigger(EnemyAnimationStates.Move);
        _animationController.ChangeAnimation((EnemyAnimationStates)_randomHittedAnimId);
    }

    public void OnExitState()
    {
        _exitDelay = 0f;
    }

    public void OnReset()
    {
        _exitDelay = 0f;
    }

    public void Tick()
    {
        Timer();
    }

    private void Timer()
    {
        _exitDelay -= Time.deltaTime;
    }

    public void ConditionCheck()
    {
        _conditions.IsSatisfied();
    }

    public float TimeDelayToExit()
    {
        return _exitDelay;
    }

    private void StopMovement()
    {
        if (_navmeshAgent.isActiveAndEnabled)
            _navmeshAgent.isStopped = true;
    }
}
