using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class BomberAttackCondition : ICondition
{
    #region Self Variables
    #region Inject Variables
    [Inject] private EnemyManager2 _manager;
    [Inject] private EnemyPhysicsController _physicsController;
    [Inject] private StateMachineInternalSignals _stateMachineInternalSignals;
    [Inject] private EnemySettings _settings;
    #endregion
    #region Private Variables
    private Transform _myTransform, _playerTransform;
    private bool _condition;
    #endregion
    #endregion

    [Inject]
    public BomberAttackCondition(Transform playerTransform, Transform myTransform)
    {
        _playerTransform = playerTransform;
        _myTransform = myTransform;
    }

    public void IsSatisfied()
    {
        _condition = (_physicsController.IsDeath == false &&
            _manager.CurrentStateEnum != EnemyStateEnums.Attack &&
            Mathf.Abs((_playerTransform.transform.position - _myTransform.position).sqrMagnitude) < _settings.AttackDistance) 
            || (_physicsController.IsDeath);

        if (!_condition)
        {
            return;
        }

        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Attack, true);
    }
}
