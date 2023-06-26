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
        [Inject] private PlayerSignals PlayerSignals { get; set; }

        private void Awake()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                attackable.OnWeaponTriggerEnter(1);
            }
        }
    }
}