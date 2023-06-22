using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.External;
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
    [SerializeField] private bool isStarted = false;

    #endregion
    #region Private Variables
    private List<Vector3> _spawnPoints;
    private PoolSignals PoolSignals { get; set; }
    private CoreGameSignals CoreGameSignals { get; set; }
    private LevelSignals LevelSignals { get; set; }
    private int _levelId = 0;
    private float _enemySpawnDelay = 3, _timer;
    private int _killedEnemiesCount = 0;
    private int _killedEnemyAmountToPassLevel = 5;
    private Settings _mySettings;

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
        _spawnPoints = new List<Vector3>() { new Vector3(0,0,30), new Vector3(5, 0, 32), new Vector3(8, 0, 35) };
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
        isStarted = true;
        _levelId = LevelSignals.onGetLevelId();
        _killedEnemyAmountToPassLevel = _mySettings._killedEnemyAmountToPassLevelList[_levelId];
    }
    private void OnLevelSuccessful()
    {
        isStarted = false;
    }
    private void OnLevelFailed()
    {
        isStarted = false;
    }
    private void OnRestart()
    {
        _killedEnemiesCount = 0;
    }

    private void OnEnemyDie()
    {
        ++_killedEnemiesCount;
        if (_killedEnemiesCount.Equals(_killedEnemyAmountToPassLevel))
        {
            CoreGameSignals.onLevelSuccessful?.Invoke();
        }
    }
    public void Tick()
    {
        if (!isStarted)
        {
            return;
        }
       
        _timer -= (Time.deltaTime);
        if (_timer > 0)
        {
            return;
        }
        
        GameObject enemy = PoolSignals.onGetObject(PoolEnums.Enemy, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
        enemy.SetActive(true);
        _timer = _enemySpawnDelay;
    }
    [Serializable]
    public class Settings
    {
        public List<int> _killedEnemyAmountToPassLevelList;
    }
}
