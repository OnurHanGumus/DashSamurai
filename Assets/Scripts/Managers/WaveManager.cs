using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;
using System;
using UnityEngine.Events;
using System.Threading.Tasks;

public class WaveManager : IInitializable
{
    [Inject] private LevelSignals LevelSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private ITimer WaveTimer { get; set; }
    [Inject] public CD_EnemySpawn MySettings { get; set; }

    private int _waveId = 0;


    private void SubscribeSignals()
    {
        CoreGameSignals.onPlay += OnEnemiesCleared;
        //LevelSignals.onGetWaveId += OnGetWaveId;
    }

    public void Initialize()
    {
        SubscribeSignals();

    }

    private void OnEnemiesCleared()
    {
        StartWave(_waveId++);
    }

    private async Task StartWave(int id)
    {
        await Task.Delay(TimeSpan.FromSeconds(MySettings.TimeBetweenWaves));
        WaveTimer.Reset();

        WaveTimer.SetTimer(MySettings.Waves[id].WaveDuration);
        WaveTimer.StartTimer();
    }

    public int OnGetWaveId()
    {
        return _waveId;
    }
}
