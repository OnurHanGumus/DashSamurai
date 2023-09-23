using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoisonAbility : AbilityBase
{
    #region Self Variables
    #region Injected Variables

    #endregion

    #region Public Variables

    #endregion
    #region Serialized Variables

    #endregion
    #region Private Variables
    private SphereCollider _particleCollider;
    #endregion
    #endregion

    [Inject]
    public PoisonAbility(GameObject particle, SphereCollider sphereCollider)
    {
        _collectableEnum = CollectableEnums.Poison;
        _playerParticle = particle;
        _particleCollider = sphereCollider;
    }

    public override void SetName()
    {
        _name = "Poison Defance";
    }

    public override void Activated()
    {
        Debug.Log("Poison is Activated");
        base.Activated();
    }

    public override void Tick()
    {
        base.Tick();

        _particleCollider.radius += Time.deltaTime;
        if (_particleCollider.radius > 1)
        {
            _particleCollider.radius = 0;
        }
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
