using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class AnyCondition : ICondition
{
    private EnemyPhysicsController _physicsController;
    private StateMachineInternalSignals _stateMachineInternalSignals;
    private bool _condition;

    [Inject]
    public AnyCondition(EnemyPhysicsController physicsController, StateMachineInternalSignals stateMachineInternalSignals)
    {
        _physicsController = physicsController;
        _stateMachineInternalSignals = stateMachineInternalSignals;
    }

    public void IsSatisfied()
    {
        _condition = _physicsController.IsDeath == false &&
            _physicsController.IsHitted;

        if (!_condition)
        {
            return;
        }

        _physicsController.IsHitted = false;
        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Any, false);
    }
}
