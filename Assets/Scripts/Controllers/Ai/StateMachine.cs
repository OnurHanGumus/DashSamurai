using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class StateMachine
{
    private StateMachineInternalSignals _stateMachineInternalSignals { get; set; }

    private IState _currentState;
    private IState[] _stateList;

    private bool _isBusy = false;
    private EnemyManager2 _manager;

    [Inject]
    public StateMachine(EnemyManager2 manager, StateMachineInternalSignals stateMachineInternalSignals, params IState[] states)
    {
        _stateMachineInternalSignals = stateMachineInternalSignals;
        _stateList = states;
        _manager = manager;

        _stateMachineInternalSignals.onChangeState += OnChangeState;
    }

    private void OnChangeState(EnemyStateEnums newState, bool checkForDelay = true)
    {
        ChangeStateControl(newState, checkForDelay);
    }

    private async Task ChangeStateControl(EnemyStateEnums newState, bool checkForDelay = true)
    {
        if (_isBusy && checkForDelay)
        {
            return;
        }

        _isBusy = true;

        if (checkForDelay)
        {
            float duration;
            duration = GetRemainTimeToChangeState();
            await Task.Delay(System.TimeSpan.FromSeconds(duration));
        }

        _manager.CurrentStateEnum = newState;
        ChangeState(newState);
        _isBusy = false;
    }

    //private async Task ChangeStateControl(EnemyStateEnums newState, bool checkForDelay = true)
    //{
    //    Debug.Log(_isBusy);

    //    if (_isBusy)
    //    {
    //        return;
    //    }
    //    Debug.Log(newState);
    //    _isBusy = true;

    //    float duration;
    //    duration = GetRemainTimeToChangeState();

    //    _manager.CurrentStateEnum = newState;
    //    ChangeState(newState);


    //    await Task.Delay(System.TimeSpan.FromSeconds(duration));
    //    _isBusy = false;
    //}


    public void ChangeState(EnemyStateEnums newState)
    {
        _currentState.OnExitState();
        _currentState = _stateList[(int)newState];
        _currentState.OnEnterState();
    }

    public void InitMachine(EnemyStateEnums firstState)
    {
        _currentState = _stateList[(int) firstState];
        _currentState.OnEnterState();
    }

    public void Tick()
    {
        if (_currentState == null)
        {
            return;
        }

        _currentState.Tick();
        _currentState.ConditionCheck();
    }

    public float GetRemainTimeToChangeState()
    {
        return _currentState.TimeDelayToExit();
    }

    public void OnResetCurrentState()
    {
        if (_currentState != null)
        {
            _currentState.OnReset();
        }
        _isBusy = false;
    }
}