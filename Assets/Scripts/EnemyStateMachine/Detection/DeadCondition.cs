using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class DeadCondition : ICondition
{
    private EnemyPhysicsController _physicsController;
    private StateMachineInternalSignals _stateMachineInternalSignals;
    private bool _condition;

    [Inject]
    public DeadCondition(EnemyPhysicsController physicsController, StateMachineInternalSignals stateMachineInternalSignals)
    {
        _physicsController = physicsController;
        _stateMachineInternalSignals = stateMachineInternalSignals;
    }

    public void IsSatisfied()
    {
        _condition =
            _physicsController.IsDeath;

        if (!_condition)
        {
            return;
        }

        _stateMachineInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Dead, false);
    }
}
