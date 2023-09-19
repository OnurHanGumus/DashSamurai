using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShieldAbility : AbilityBase
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
    public ShieldAbility(GameObject particle)
    {
        _collectableEnum = CollectableEnums.Shield;
        _playerParticle = particle;
    }

    public override void Activated()
    {
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
}
