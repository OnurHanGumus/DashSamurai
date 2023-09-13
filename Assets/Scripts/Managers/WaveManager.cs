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
    #region Self Variables
    #region Injected Variables
    [Inject] public CD_EnemySpawn MySettings { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private ITimer WaveTimer { get; set; }
    [Inject] private LevelSignals LevelSignals { get; set; }

    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private int _killedEnemiesCount = 0;
    private int _spawnedEnemyCount = 0;
    private int _waveId;

    #endregion
    #endregion

    #region Event Subscriptions
    private void SubscribeSignals()
    {
        CoreGameSignals.onPlay += OnPlay;
        LevelSignals.onEnemyInitialized += OnEnemyInitialized;
        LevelSignals.onEnemyDied += OnEnemyDie;
    }

    #endregion
    public void Initialize()
    {
        SubscribeSignals();
        _waveId = LevelSignals.onGetLevelId();
    }

    private void OnPlay()
    {
        _killedEnemiesCount = 0;
        _spawnedEnemyCount = 0;

        _waveId = LevelSignals.onGetLevelId();
        StartWave(_waveId);
    }

    private void StartWave(int id)
    {
        WaveTimer.Reset();

        WaveTimer.SetTimer(MySettings.Waves[id].WaveDuration);
        WaveTimer.StartTimer();
    }

    private void OnEnemyInitialized()
    {
        ++_spawnedEnemyCount;
    }

    private void OnEnemyDie()
    {
        ++_killedEnemiesCount;
        if (!WaveTimer.IsTimerEnded())
        {
            return;
        }

        if (_killedEnemiesCount.Equals(_spawnedEnemyCount))
        {
            CoreGameSignals.onLevelSuccessful?.Invoke();
        }
    }
}
