using UnityEngine;
using Zenject;
using Signals;
using System.Threading.Tasks;
using System;

public class BossSpawnManager : IInitializable
{
    #region Self Variables
    #region Injected Variables
    [Inject] public CD_EnemySpawn MySettings { get; set; }
    [Inject] private ITypeSelector EnemyTypeSelector { get; set; }
    [Inject] private IPointSelector SpawnPointSelector { get; set; }
    [Inject] private ITimer WaveTimer { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }

    #endregion

    #region Public Variables
    public int WaveId = 0;

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private PoolSignals PoolSignals { get; set; }
    private LevelSignals LevelSignals { get; set; }
    private int _time;
    private Vector3 _position;

    #endregion
    #endregion

    public BossSpawnManager(PoolSignals poolSignals, LevelSignals levelSignals)
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
        WaveTimer.onTimeUpdated += OnTimeUpdated;
    }
    #endregion

    public void Initialize() //Start
    {
        SubscribeEvents();
    }

    private void OnWaveStarted()
    {
        WaveId = LevelSignals.onGetLevelId();
        EnemyTypeSelector.SetRange();
    }

    private void OnTimeUpdated(int time)
    {
        if (!WaveTimer.IsStarted())
        {
            return;
        }

        foreach (var i in MySettings.Waves[WaveId].SpawnBosses)
        {
            _time = (int)WaveTimer.GetTime();
            if (time == i.SecondToInstantiate)
            {
                _position = i.Position;

                GameObject collectable = PoolSignals.onGetObject((PoolEnums)Enum.Parse(typeof(PoolEnums), i.BossType.ToString()), _position);
                collectable.SetActive(true);
                LevelSignals.onEnemyInitialized?.Invoke();

            }
        }


    }
}
