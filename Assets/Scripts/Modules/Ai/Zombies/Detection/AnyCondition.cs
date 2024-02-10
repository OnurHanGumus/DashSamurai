using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Controllers;

public class AnyCondition : ICondition
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
    public AnyCondition()
    {

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
