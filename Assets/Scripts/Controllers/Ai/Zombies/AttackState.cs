using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Enums;
using Data.MetaData;

public class AttackState : IState
{
    #region Self Variables

    #region Inject Variables
    [Inject] private NavMeshAgent _navmeshAgent;
    [Inject] private EnemySettings _settings;
    [Inject] private EnemyAnimationController _animationController;
    #endregion

    #region Public Variables

    #endregion

    #region SerializeField Variables
    #endregion

    #region Private Variables

    private Transform _playerTransform, _myTransform;
    private bool _isAttacking = false;
    private float _attackDelay = 0f;
    private Conditions _conditions;
    
    #endregion
    #endregion

    public AttackState(Transform playerTransform, Transform myTransform, Conditions conditions)
    {
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _conditions = conditions;
    }

    public void OnEnterState()
    {
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
        _animationController.ResetTrigger(EnemyAnimationStates.Attack1);

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
