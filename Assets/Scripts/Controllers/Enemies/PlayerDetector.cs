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
    public class PlayerDetector : MonoBehaviour
    {
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }

        private void Awake()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);
            }
        }
    }
}