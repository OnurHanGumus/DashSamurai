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
    public class Detector : MonoBehaviour
    {
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
        [Inject(Id = "Player")] private Transform playerTransform;

        [SerializeField] private EnemyManager2 manager;

        private void Awake()
        {

        }

        private void Update()
        {
            if (manager.CurrentStateEnum != EnemyStateEnums.Attack && Mathf.Abs((playerTransform.transform.position - transform.position).sqrMagnitude) < 2f)
            {
                EnemyInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Attack);
            }
            else if (manager.CurrentStateEnum != EnemyStateEnums.Move && Mathf.Abs((playerTransform.transform.position - transform.position).sqrMagnitude) > 2f)
            {
                EnemyInternalSignals.onChangeState?.Invoke(EnemyStateEnums.Move);
            }
        }
    }
}