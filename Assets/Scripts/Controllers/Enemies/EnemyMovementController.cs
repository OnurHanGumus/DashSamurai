using Data.MetaData;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class EnemyMovementController : MonoBehaviour
    {

        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private EnemySettings EnemySettings { get; set; }

        private Settings _mySettings;
   
        private void Awake()
        {
            _mySettings = EnemySettings.EnemyMovementSettings;
        }
        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}