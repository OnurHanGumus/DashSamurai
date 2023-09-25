using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BomberTimer : MonoBehaviour, ITimer
{
    #region Self Variables
    #region Injected Variables
    [Inject] private EnemySettings _enemySettings { get; set; }
    #endregion

    #region Public Variables
    public Action<int> onTimeUpdated { get; set; }


    #endregion
    #region Serialized Variables
    [SerializeField] private Component attackableComponent;
    [SerializeField] private ParticleSystem fireParticle;

    #endregion
    #region Private Variables
    private float _timer = 0;
    private bool _isStarted = false;
    private bool _isTimerEnded = false;
    private int _lastTimeValue;
    ITimer _itimer;
    private Vector3 _particleInitializeScale;


    #endregion
    #endregion

    private void Awake()
    {
        _particleInitializeScale = fireParticle.transform.localScale;
    }

    private void OnEnable()
    {
        _itimer = this;
        _itimer.SetTimer((int)_enemySettings.AttackDelay);
        _itimer.StartTimer();
        fireParticle.transform.localScale = _particleInitializeScale;
    }

    private void OnDisable()
    {
        _itimer.StopTimer();
        _itimer.Reset();
    }

    void ITimer.SetTimer(int value)
    {
        _timer = value;
        _lastTimeValue = value;
    }

    float ITimer.GetTime()
    {
        return _timer;
    }

    void Update()
    {
        if (!_isStarted)
        {
            return;
        }

        if (_isTimerEnded)
        {
            return;
        }

        _timer -= Time.deltaTime;

        if (_lastTimeValue - (int)_timer == 1)
        {
            _lastTimeValue = (int)_timer;
            fireParticle.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f); 
        }

        if (_timer <= 1)
        {
            _isTimerEnded = true;
            ((IAttackable)attackableComponent).OnWeaponTriggerEnter(500);

            return;
        }
    }

    void ITimer.StartTimer()
    {
        _isStarted = true;
    }

    void ITimer.StopTimer()
    {
        _isStarted = false;
    }

    bool ITimer.IsStarted()
    {
        return _isStarted;
    }

    bool ITimer.IsTimerEnded()
    {
        return _isTimerEnded;
    }

    void ITimer.Reset()
    {
        _isStarted = false;
        _isTimerEnded = false;
    }
}
