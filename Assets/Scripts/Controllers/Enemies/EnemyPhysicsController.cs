using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Signals;
using System.Threading.Tasks;
using Components.Enemies;
using Data.MetaData;
using System;

public class EnemyPhysicsController : MonoBehaviour, IAttackable
{
    #region Self Variables

    #region Inject Variables
    [Inject] private LevelSignals _levelSignals { get; set; }
    [Inject] private PoolSignals _poolSignals { get; set; }
    [Inject] private EnemyInternalSignals _enemyInternalSignals { get; set; }
    [Inject] private EnemySettings _mySettings;
    public bool IsMoving { get => false; }
    public bool IsHitted { get; set; }
    public bool IsDeath { get; set; }

    #endregion

    #region Serialized Variables
    [SerializeField] private GameObject enemy;
    [SerializeField] private ParticleEnums dieParticle;


    #endregion
    #region Private Variables
    private int _enemyHits;
    private int _randomHittedAnimId = 0;
    #endregion
    #endregion

    private void OnEnable()
    {
        IsDeath = false;
        _enemyHits = _mySettings.Health;
    }

    void IAttackable.OnWeaponTriggerEnter(int value)
    {
        if (IsDeath)
        {
            return;
        }

        _enemyHits -= value;

        if (_enemyHits <= 0)
        {
            IsDeath = true;
            _enemyInternalSignals.onDeath?.Invoke(this);
            _levelSignals.onEnemyDied?.Invoke();
            //LevelSignals.onEnemyDied.Invoke();
            if (dieParticle.ToString() != "None")
            {
                _poolSignals.onGetObject(((PoolEnums)Enum.Parse(typeof(PoolEnums), dieParticle.ToString())), transform.position).SetActive(true); ;
            }

            DieDelay();
        }
        else
        {
            IsHitted = true;
        }
    }

    private async Task DieDelay()
    {
        await Task.Delay(System.TimeSpan.FromSeconds(_mySettings.DeathDuration));

        enemy.SetActive(false);
    }
}