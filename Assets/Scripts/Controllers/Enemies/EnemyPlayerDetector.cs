using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;
using Signals;
using System.Threading.Tasks;

namespace Controllers
{
    public class EnemyPlayerDetector : MonoBehaviour
    {
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }

        [SerializeField] private Collider collider;

        private bool _isPlayerInRange = false;

        private void Awake()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AttackAnimationDelay();
            }
        }

        private async Task AttackAnimationDelay()
        {
            EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Attack1);
            EnemyInternalSignals.onAttack?.Invoke();
            await Task.Delay(2300);
            collider.gameObject.SetActive(false);
            collider.gameObject.SetActive(true);
        }
    }
}