using Data.MetaData;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Components.Players
{
    public class EnemyMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Inject Variables
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private EnemySettings EnemySettings { get; set; }
        #endregion

        #region Public Variables

        #endregion

        #region SerializeField Variables
        #endregion

        #region Private Variables
        private Rigidbody _rig;
        private Transform _playerTransform;
        private NavMeshAgent _navmeshAgent;

        #endregion
        #endregion
        

        private Settings _mySettings;
   
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _mySettings = EnemySettings.EnemyMovementSettings;
            _rig = GetComponent<Rigidbody>();
            _navmeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _playerTransform = PlayerSignals.onGetTransform();
        }

        private void Update()
        {
            //MoveToDefaultTarget(_playerTransform);
            NavMeshMove(_playerTransform);
        }

        public void MoveToDefaultTarget(Transform target)
        {
            Vector3 distance = target.position - transform.position;
            int isReachedToOffset = (distance).magnitude > _mySettings.DistanceToPlayer ? 1 : 0;
            Vector3 direction = (distance).normalized;

            transform.LookAt(target, Vector3.up);
            _rig.velocity = direction * _mySettings.Speed * isReachedToOffset;
        }

        private void NavMeshMove(Transform target)
        {
            _navmeshAgent.destination = target.position;
            ;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
            [SerializeField] public float DistanceToPlayer = 0.5f;
        }
    }
}