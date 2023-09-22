using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class AbilityBase : IAbility, ITickable
{
    #region Self Variables
    #region Injected Variables
    [Inject] protected AbilitySignals _abilitySignals { get; set; }
    [Inject] protected AbilitySettings _abilitySettings { get; set; }

    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    protected float _duration;
    protected CollectableEnums _collectableEnum;
    protected GameObject _playerParticle;

    #endregion
    #endregion

    public virtual void Activated()
    {
        _abilitySettings.AbilityDatas[(int)_collectableEnum].IsActivated = true;
        _duration = _abilitySettings.AbilityDatas[(int)_collectableEnum].Duration;
        _playerParticle.SetActive(true);
    }

    public virtual void Tick()
    {
        if (!_abilitySettings.AbilityDatas[(int)_collectableEnum].IsActivated)
        {
            return;
        }
        _duration -= Time.deltaTime;

        if ((int)_duration == 0)
        {
            Deactivated();
        }
    }

    public virtual void Deactivated()
    {
        _abilitySettings.AbilityDatas[(int)_collectableEnum].IsActivated = false;
        _playerParticle.SetActive(false);
    }
}
