using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Components.Enemies;
using Signals;

namespace Controllers
{
    public class PlayerEnemyDetector : MonoBehaviour
    {
        [Inject] private AbilitySettings _abilitySettings;
        [Inject] private AudioSignals _audioSignals;
        [Inject] private PoolSignals _poolSignals { get; set; }
        [SerializeField] private ParticleEnums hitParticle;
        private AbilityData _abilityData;

        private void Awake()
        {
            _abilityData = _abilitySettings.AbilityDatas[(int)CollectableEnums.KillOneDash];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (attackable.Health <= 0)
                {
                    return;
                }
                attackable.OnWeaponTriggerEnter(1 + ((_abilityData.IsActivated ? 1 : 0) * _abilityData.Value));
                if (hitParticle.ToString() != "None")
                {
                    _poolSignals.onGetObject((PoolEnums)Enum.Parse(typeof(PoolEnums), hitParticle.ToString()), new Vector3(other.transform.position.x, other.transform.position.y + 1f + other.transform.position.z)).SetActive(true);
                }

                _audioSignals.onPlaySound?.Invoke(AudioSoundEnums.Cut);
            }
        }
    }
}