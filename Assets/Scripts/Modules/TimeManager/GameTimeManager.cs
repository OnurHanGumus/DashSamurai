using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameTimeManager : IInitializable
{
    public bool IsStopped = false;

    public void Initialize()
    {
        SubscribeSignals();
    }

    private void SubscribeSignals()
    {

    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
