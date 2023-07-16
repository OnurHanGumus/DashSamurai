using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StateMachine
{
    private IState _currentState;
    private IState[] _stateList;

    [Inject]
    public StateMachine(params IState[] states)
    { 
        _stateList = states;
    }

    public void ChangeState(EnemyStateEnums newState)
    {
        _currentState.OnExitState();
        _currentState = _stateList[(int)newState];
        _currentState.OnEnterState();
    }

    public void InitMachine(EnemyStateEnums firstState)
    {
        _currentState = _stateList[(int) firstState];
    }

    public void Tick()
    {
        if (_currentState == null)
        {
            return;
        }

        _currentState.Tick();
    }

    public void OnResetCurrentState()
    {
        if (_currentState != null)
        {
            _currentState.OnReset();
        }
    }
}
