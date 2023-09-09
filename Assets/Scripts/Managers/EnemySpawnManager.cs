using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Signals;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
using Enums;

public class EnemySpawnManager : IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] public CD_EnemySpawn MySettings { get; set; }
    [Inject] private ITypeSelector EnemyTypeSelector { get; set; }
    [Inject] private IPointSelector SpawnPointSelector { get; set; }
    [Inject] private ITimer WaveTimer { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private PlayerSignals PlayerSignals { get; set; }

    #endregion

    #region Public Variables
    public int WaveId = 0;

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private PoolSignals PoolSignals { get; set; }
    private LevelSignals LevelSignals { get; set; }
    private int _killedEnemiesCount = 0;
    private int _spawnedEnemyCount = 0;
    private float _currentSpawnDelay = 0f;
    private EnemyTypeEnums _randomEnemyType;
    private bool _isPlayerDead = false;
    #endregion
    #endregion

    public EnemySpawnManager(PoolSignals poolSignals, LevelSignals levelSignals)
    {
        //Debug.Log("Const"); //Awake
        PoolSignals = poolSignals;
        LevelSignals = levelSignals;

        AwakeInit();
    }

    private void AwakeInit()
    {

    }

    #region Event Subscriptions
    private void SubscribeEvents()
    {

        CoreGameSignals.onPlay += OnWaveStarted;
        PlayerSignals.onDied += OnPlayerDied;
        LevelSignals.onEnemyDied += OnEnemyDie;
    }
    #endregion

    public void Initialize() //Start
    {
        SubscribeEvents();
    }

    private void OnWaveStarted()
    {
        Reset();
        _isPlayerDead = false;
        WaveId = LevelSignals.onGetLevelId();
        EnemyTypeSelector.SetRange();

        SpawnEnemy();
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

    private async Task SpawnEnemy()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        while (WaveTimer.IsStarted())
        {
            _currentSpawnDelay = MySettings.Waves[WaveId].SpawnDelay.Evaluate(
                (MySettings.Waves[WaveId].WaveDuration - WaveTimer.GetTime()) / MySettings.Waves[WaveId].WaveDuration);

            await Task.Delay(TimeSpan.FromSeconds(_currentSpawnDelay / MySettings.Waves[WaveId].WaveScale));

            if (WaveTimer.IsTimerEnded() || _isPlayerDead)
            {
                break;
            }

            float startTime = Time.time;
            float currentTime = startTime;
            while (currentTime == startTime)
            {
                currentTime += Time.deltaTime;
                await Task.Yield();
            }

            _randomEnemyType = MySettings.Waves[WaveId].SpawnableEnemies[EnemyTypeSelector.GetType()].enemyType;
            GameObject enemy = PoolSignals.onGetObject((PoolEnums)Enum.Parse(typeof(PoolEnums), _randomEnemyType.ToString()), SpawnPointSelector.GetPoint(5f));
            enemy.SetActive(true);
            ++_spawnedEnemyCount;
        }
    }

    private void OnPlayerDied()
    {
        _isPlayerDead = true;
    }

    private void Reset()
    {
        _killedEnemiesCount = 0;
        _spawnedEnemyCount = 0;
    }
}
