using UnityEngine;
using Zenject;
using Signals;
using System.Threading.Tasks;
using System;

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
        CoreGameSignals.onRestart += OnRestartLevel;
        PlayerSignals.onDied += OnPlayerDied;
    }
    #endregion

    public void Initialize() //Start
    {
        SubscribeEvents();
    }

    private void OnWaveStarted()
    {
        ResetValues();
        WaveId = LevelSignals.onGetLevelId();
        EnemyTypeSelector.SetRange();

        SpawnEnemy();
    }

    private async Task SpawnEnemy()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));

        while (WaveTimer.IsStarted())
        {
            _currentSpawnDelay = MySettings.Waves[WaveId].SpawnDelay.Evaluate(
                (MySettings.Waves[WaveId].WaveDuration - WaveTimer.GetTime()) / MySettings.Waves[WaveId].WaveDuration);

            await Task.Delay(TimeSpan.FromSeconds(_currentSpawnDelay / MySettings.Waves[WaveId].WaveScale));



            float startTime = Time.time;
            float currentTime = startTime;
            while (currentTime == startTime)
            {
                currentTime += Time.deltaTime;
                await Task.Yield();
            }

            if (WaveTimer.IsTimerEnded() || _isPlayerDead || !WaveTimer.IsStarted())
            {
                break;
            }

            _randomEnemyType = MySettings.Waves[WaveId].SpawnableEnemies[EnemyTypeSelector.GetType()].EnemyType;
            GameObject enemy = PoolSignals.onGetObject((PoolEnums)Enum.Parse(typeof(PoolEnums), _randomEnemyType.ToString()), SpawnPointSelector.GetPoint(5f));
            enemy.SetActive(true);

            LevelSignals.onEnemyInitialized?.Invoke();
        }
    }

    private void OnPlayerDied()
    {
        _isPlayerDead = true;
    }

    private void ResetValues()
    {
        _isPlayerDead = false;
    }

    private void OnRestartLevel()
    {
        WaveTimer.StopTimer();
        ResetValues();
    }
}
