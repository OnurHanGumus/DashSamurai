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
        }

        private void Start()
        {
            _playerTransform = PlayerSignals.onGetTransform();
        }

        private void Update()
        {
            MoveToDefaultTarget(_playerTransform);
        }

        public void MoveToDefaultTarget(Transform lookAtObject)
        {
            Vector3 distance = lookAtObject.position - transform.position;
            int isReachedToOffset = (distance).magnitude > _mySettings.DistanceToPlayer ? 1 : 0;
            Vector3 direction = (distance).normalized;

            transform.LookAt(lookAtObject, Vector3.up);
            _rig.velocity = direction * _mySettings.Speed * isReachedToOffset;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
            [SerializeField] public float DistanceToPlayer = 0.5f;
        }
    }
}