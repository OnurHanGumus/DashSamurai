using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITimer
{
    Action<int> onTimeUpdated { get; set; }
    void SetTimer(int value);
    float GetTime();
    void StartTimer();
    void StopTimer();
    bool IsStarted();
    bool IsTimerEnded();
    void Reset();
}
