using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Components.Enemies;
using Signals;
using System.Threading.Tasks;

namespace Controllers
{
    public class EnemyPlayerDetector : MonoBehaviour
    {
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IAttackable attackable))
            {
                if (attackable.IsMoving)
                {
                    return;
                }
                AttackAnimationDelay();
            }
        }

        private async Task AttackAnimationDelay()
        {
            EnemyInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Attack);
            await Task.Delay(TimeSpan.FromSeconds(2.3f));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EnemyInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Move);
            }
        }
    }
}