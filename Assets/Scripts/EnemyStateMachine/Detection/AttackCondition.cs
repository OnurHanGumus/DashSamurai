using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class AttackCondition : ICondition
{
    private Transform _myTransform, _playerTransform;
    private EnemyManager2 _manager;
    private StateMachineInternalSignals _stateMachineInternalSignals;
    private EnemyPhysicsController _physicsController;
    private bool _condition;

    [Inject]
    public AttackCondition(EnemyManager2 manager, EnemyPhysicsController physicsController, Transform playerTransform, Transform myTransform, StateMachineInternalSignals stateMachineInternalSignals)
    {
        _manager = manager;
        _physicsController = physicsController;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _stateMachineInternalSignals = stateMachineInternalSignals;
    }

    public void IsSatisfied()
    {
        _condition = _physicsController.IsDeath == false &&
            _manager.CurrentStateEnum != EnemyStateEnums.Attack &&
            Mathf.Abs((_playerTransform.transform.position - _myTransform.position).sqrMagnitude) < 1f;

        if (!_condition)
        {
            return;
        }

        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Attack, true);
    }
}
