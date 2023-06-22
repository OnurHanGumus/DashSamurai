using System;
using System.Collections;
using System.Collections.Generic;
using Components.Enemies;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;
using Enums;
using Signals;

public class BulletPhysicsController : MonoBehaviour, IPoolType
{
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private PoolSignals PoolSignals { get; set; }

    private Settings _mySettings;

    [Inject]
    public void Constractor(BulletSettings bulletSettings)
    {
        _mySettings = bulletSettings.BulletCollisionDetectorSettings;
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTime());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable attackable))
        {
            PlayerSignals.onEnemyShooted?.Invoke(attackable);
            DespawnSignal();
            attackable.OnWeaponTriggerEnter();
            GameObject particle = PoolSignals.onGetObject(PoolEnums.Explosion, transform.position);
            particle.SetActive(false);
            particle.transform.position = transform.position;
            particle.SetActive(true);
        }
    }

    private IEnumerator BulletLifeTime()
    {
        yield return new WaitForSeconds(_mySettings.BulletLifeTime);
        DespawnSignal();
    }

    private void DespawnSignal()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public class Settings
    {
        [SerializeField] public float BulletLifeTime = 1f;
    }
}
