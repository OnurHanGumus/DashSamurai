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

    private int _waveId;


    private void SubscribeSignals()
    {
        CoreGameSignals.onPlay += OnEnemiesCleared;
        //LevelSignals.onGetWaveId += OnGetWaveId;
    }

    public void Initialize()
    {
        SubscribeSignals();
        _waveId = LevelSignals.onGetLevelId();
    }

    private void OnEnemiesCleared()
    {
        _waveId = LevelSignals.onGetLevelId();
        StartWave(_waveId);
    }

    private void StartWave(int id)
    {
        WaveTimer.Reset();

        WaveTimer.SetTimer(MySettings.Waves[id].WaveDuration);
        WaveTimer.StartTimer();
    }
}
