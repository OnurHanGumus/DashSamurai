using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;
using Signals;

namespace Controllers
{
    public class EnemyDamageDetector : MonoBehaviour
    {
        [Inject] private EnemySettings EnemySettings { get; set; }
        [Inject] private AudioSignals _audioSignals { get; set; }

        [SerializeField] private AudioSoundEnums hitSound;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.OnWeaponTriggerEnter(EnemySettings.AttackPower);

                _audioSignals.onPlaySound?.Invoke(hitSound);
            }
        }
    }
}