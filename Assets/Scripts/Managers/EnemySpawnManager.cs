using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Zenject;
using Signals;
using System.Threading.Tasks;
using System;
using Random = UnityEngine.Random;
using Data.MetaData;

public class EnemySpawnManager: ITickable, IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] private EnemySpawnSettings EnemySpawnSettings { get; set; }

    #endregion

    #region Serialized Variables
    [SerializeField] private bool IsStarted = false;

    #endregion
    #region Private Variables
    private List<Vector3> _spawnPoints;
    private PoolSignals PoolSignals { get; set; }
    private CoreGameSignals CoreGameSignals { get; set; }
    private LevelSignals LevelSignals { get; set; }
    private int _levelId = 0;
    private int _killedEnemiesCount = 0;
    private int _spawnedEnemyCount = 0;
    private float _timer;
    private Settings _mySettings;
    private bool _isReachedToSpawnNumber = false;

    #endregion
    #endregion

    public EnemySpawnManager(PoolSignals poolSignals, CoreGameSignals coreGameSignals, LevelSignals levelSignals)
    {
        //Debug.Log("Const"); //Awake
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
        LevelSignals = levelSignals;
        Awake();
    }

    private void Awake()
    {
        Init();
        SubscribeEvents();
    }

    private void Init()
    {
        _spawnPoints = new List<Vector3>() { new Vector3(2,0.5f,-2), new Vector3(0, 0.5f, 0), new Vector3(-2, 0.5f, 2) };
    }

    public void Initialize()
    {
        //Start
        _mySettings = EnemySpawnSettings.EnemyManagerSpawnSettings;
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();

    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onPlay += OnPlay;
        CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.onLevelFailed += OnLevelFailed;
        CoreGameSignals.onRestart += OnRestart;

        LevelSignals.onEnemyDied += OnEnemyDie;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onPlay -= OnPlay;
        CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.onRestart -= OnRestart;

        LevelSignals.onEnemyDied -= OnEnemyDie;

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnPlay()
    {
        IsStarted = true;
        _levelId = LevelSignals.onGetLevelId();
        _timer = _mySettings.Episodes[_levelId].WaveDuration;

        SpawnEnemy();
    }

    private void OnLevelSuccessful()
    {
        IsStarted = false;
    }

    private void OnLevelFailed()
    {
        IsStarted = false;
    }

    private void OnRestart()
    {
        _killedEnemiesCount = 0;
        _spawnedEnemyCount = 0;
        _isReachedToSpawnNumber = false;
    }

    private void OnEnemyDie()
    {
        ++_killedEnemiesCount;
        if (!_isReachedToSpawnNumber)
        {
            return;
        }

        if (_killedEnemiesCount.Equals(_spawnedEnemyCount))
        {
            CoreGameSignals.onLevelSuccessful?.Invoke();
        }
    }

    public void Tick()
    {
        if (!IsStarted)
        {
            return;
        }

        if (_isReachedToSpawnNumber)
        {
            return;
        }

        _timer -= Time.deltaTime;
        LevelSignals.onTimerChanged?.Invoke((int) _timer);

        if (_timer <= 1)
        {
            _isReachedToSpawnNumber = true;
            return;
        }
    }

    private async Task SpawnEnemy()
    {
        while (IsStarted)
        {
            if (_isReachedToSpawnNumber)
            {
                break;
            }

            await Task.Delay((int) (1000 * _mySettings.Episodes[_levelId].SpawnDelay.Evaluate((_mySettings.Episodes[_levelId].WaveDuration - _timer) / _mySettings.Episodes[_levelId].WaveDuration)));
            GameObject enemy = PoolSignals.onGetObject(PoolEnums.Enemy, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
            enemy.SetActive(true);
            ++_spawnedEnemyCount;
        }
    }

    [Serializable]
    public class Settings
    {
        public List<EpisodeSpawnSettings> Episodes;

        [System.Serializable]
        public struct EpisodeSpawnSettings
        {
            public List<GameObject> SpawnableEnemies;
            public AnimationCurve SpawnDelay;
            public int WaveDuration;
        }
    }
}
