using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;
using System;

public class WaveTimer : ITimer, ITickable
{
    #region Self Variables
    #region Injected Variables
    #endregion

    #region Public Variables
    public Action<int> onTimeUpdated { get; set; }


    #endregion
    #region Serialized Variables


    #endregion
    #region Private Variables
    private float _timer = 0;
    private bool _isStarted = false;
    private bool _isTimerEnded = false;


    #endregion
    #endregion

    void ITimer.SetTimer(int value)
    {
        _timer = value;
    }

    float ITimer.GetTime()
    {
        return _timer;
    }

    void ITickable.Tick()
    {
        if (!_isStarted)
        {
            return;
        }

        if (_isTimerEnded)
        {
            return;
        }

        _timer -= Time.deltaTime;
        Debug.Log(_timer);
        onTimeUpdated.Invoke((int)_timer);

        if (_timer <= 1)
        {
            _isTimerEnded = true;
            return;
        }
    }

    void ITimer.StartTimer()
    {
        _isStarted = true;
    }

    void ITimer.StopTimer()
    {
        _isStarted = false;
    }

    bool ITimer.IsStarted()
    {
        return _isStarted;
    }

    bool ITimer.IsTimerEnded()
    {
        return _isTimerEnded;
    }

    void ITimer.Reset()
    {
        _isStarted = false;
        _isTimerEnded = false;
    }
}
