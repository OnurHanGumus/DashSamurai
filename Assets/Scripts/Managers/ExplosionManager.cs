using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Events.External;
using Enums;
using Signals;

public class ExplosionManager : MonoBehaviour
{
    [Inject] private PoolSignals PoolSignals { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject]
    public void Construct(PoolSignals poolSignals, CoreGameSignals coreGameSignals)
    {
        PoolSignals = poolSignals;
        CoreGameSignals = coreGameSignals;
    }

    #region Event Subscriptions

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
    #endregion

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
    public class Factory : PlaceholderFactory<ExplosionManager>, IPool 
    {
        GameObject IPool.OnCreate()
        {
            return base.Create().gameObject;
        }
    }
}
