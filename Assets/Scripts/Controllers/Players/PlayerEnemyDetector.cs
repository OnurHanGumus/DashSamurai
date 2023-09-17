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
    public class PlayerEnemyDetector : MonoBehaviour
    {
        [Inject] private AbilitySettings _abilitySettings;
        private AbilityData _abilityData;

        private void Awake()
        {
            _abilityData = _abilitySettings.AbilityDatas[(int)CollectableEnums.KillOneDash];
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.OnWeaponTriggerEnter(1 + ((_abilityData.IsActivated ? 1 : 0) * _abilityData.Value));
            }
        }
    }
}