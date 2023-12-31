using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DashAbility : AbilityBase
{
    #region Self Variables
    #region Injected Variables

    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    #endregion
    #endregion

    [Inject]
    public DashAbility(GameObject particle)
    {
        _collectableEnum = CollectableEnums.KillOneDash;
        _playerParticle = particle;
    }

    public override void SetName()
    {
        _name = "Kill only one Dash";
    }

    public override void Activated()
    {
        Debug.Log("Dash is Activated");
        base.Activated();
    }

    public override void Tick()
    {
        base.Tick();
    }

    public override void Deactivated()
    {
        base.Deactivated();
    }

    public override string GetName()
    {
        return _name;
    }
}
