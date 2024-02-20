using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StaminaAbility : AbilityBase
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PlayerSettings _playerSettings;
    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private float _tempSpeed;
    #endregion
    #endregion

    [Inject]
    public StaminaAbility(GameObject particle)
    {
        _collectableEnum = CollectableEnums.EndlessStamina;
        _playerParticle = particle;
    }

    public override void SetName()
    {
        _name = "Endless stamina";
    }

    public override void Activated()
    {
        Debug.Log("Stamina is Activated");
        _tempSpeed = _playerSettings.Speed;
        _playerSettings.Speed = _abilitySettings.AbilityDatas[(int)CollectableEnums.EndlessStamina].Value;
        base.Activated();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Deactivated()
    {
        _playerSettings.Speed = _tempSpeed;
        base.Deactivated();
    }

    public override string GetName()
    {
        return _name;
    }
}
