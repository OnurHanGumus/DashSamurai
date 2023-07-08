using Components.Enemies;
using Data.MetaData;
using System;
using System.Threading.Tasks;
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

        private void OnEnable()
        {
            _navmeshAgent.isStopped = false;
        }

        private void Start()
        {
            _playerTransform = PlayerSignals.onGetTransform();
        }

        private void Update()
        {
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
        }

        public void OnDeath(IAttackable attackable)
        {
            _navmeshAgent.isStopped = true;
        }

        public void OnHitted()
        {
            _navmeshAgent.isStopped = true;
            TakeDamageDelay();
        }

        public void OnAttack()
        {
            _navmeshAgent.isStopped = true;
            TakeDamageDelay();
        }

        private async Task TakeDamageDelay()
        {
            await Task.Delay((int)(_mySettings.DelayAfterHitted * 1000));
            if (_navmeshAgent.isActiveAndEnabled)
            {
                _navmeshAgent.isStopped = false;
            }
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
            [SerializeField] public float DistanceToPlayer = 0.5f;
            [SerializeField] public float DelayAfterHitted = 1.5f;
        }
    }
}