using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class DeadCondition : ICondition
{
    #region Self Variables
    #region Inject Variables
    [Inject] private EnemyPhysicsController _physicsController;
    [Inject] private StateMachineInternalSignals _stateMachineInternalSignals;
    #endregion
    #region Private Variables
    private bool _condition;
    #endregion
    #endregion

    [Inject]
    public DeadCondition()
    {

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
