using Controllers;
using Data.MetaData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoveCondition : ICondition
{
    private Transform _myTransform, _playerTransform;
    private EnemyManager2 _manager;
    private EnemyPhysicsController _physicsController;
    private StateMachineInternalSignals _stateMachineInternalSignals;
    private bool _condition;
    private EnemySettings _settings;

    [Inject]
    public MoveCondition(EnemyManager2 manager, EnemyPhysicsController physicsController, Transform playerTransform, Transform myTransform, StateMachineInternalSignals stateMachineInternalSignals, EnemySettings settings)
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
        _condition = _physicsController.IsDeath == false &&
            _manager.CurrentStateEnum != EnemyStateEnums.Move &&
            Mathf.Abs((_playerTransform.transform.position - _myTransform.position).sqrMagnitude) > _settings.MoveToAttackDistance;

        if (!_condition)
        {
            return;
        }

        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Move, true);
    }
}
