using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;
using Data.MetaData;

public class BomberAttackCondition : ICondition
{
    private Transform _myTransform, _playerTransform;
    private EnemyManager2 _manager;
    private StateMachineInternalSignals _stateMachineInternalSignals;
    private EnemyPhysicsController _physicsController;
    private bool _condition;
    private EnemySettings _settings;

    [Inject]
    public BomberAttackCondition(EnemyManager2 manager, EnemyPhysicsController physicsController, Transform playerTransform, Transform myTransform, StateMachineInternalSignals stateMachineInternalSignals, EnemySettings settings)
    {
        _manager = manager;
        _physicsController = physicsController;
        _playerTransform = playerTransform;
        _myTransform = myTransform;
        _stateMachineInternalSignals = stateMachineInternalSignals;
        _settings = settings;
    }

    public void IsSatisfied()
    {
        _condition = (_physicsController.IsDeath == false &&
            _manager.CurrentStateEnum != EnemyStateEnums.Attack &&
            Mathf.Abs((_playerTransform.transform.position - _myTransform.position).sqrMagnitude) < _settings.AttackDistance) || _physicsController.IsDeath;

        if (!_condition)
        {
            return;
        }

        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Attack, true);
    }
}
