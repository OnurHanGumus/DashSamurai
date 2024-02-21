using Components.Enemies;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using System.Collections;
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
        private bool _isDead = false;

        #endregion
        #endregion
        

        private Settings _mySettings;
   
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            //_mySettings = EnemySettings.EnemyMovementSettings;
            _rig = GetComponent<Rigidbody>();
            _navmeshAgent = GetComponent<NavMeshAgent>();
        }

        private void OnEnable()
        {
            _navmeshAgent.isStopped = false;
            _isDead = false;
        }

        private void Start()
        {
            _playerTransform = PlayerSignals.onGetTransform();
        }

        private void Update()
        {
            NavMeshMove(_playerTransform);
            ManuelRotation();
        }

        private void NavMeshMove(Transform target)
        {
            _navmeshAgent.destination = target.position;
        }

        private void ManuelRotation()
        {
            if (_isDead)
            {
                return;
            }

            if (_navmeshAgent.isStopped)
            {
                transform.LookAt(_playerTransform);
            }
        }

        public void OnDeath(IAttackable attackable)
        {
            _isDead = true;
            _navmeshAgent.isStopped = true;
        }

        public void OnHitted()
        {
            _navmeshAgent.isStopped = true;
            TakeDamageDelay(_mySettings.DelayAfterHitted);
        }

        public void OnAttack()
        {
            _navmeshAgent.isStopped = true;
            TakeDamageDelay(2.3f);
        }

        private async Task TakeDamageDelay(float value)
        {
            await Task.Delay((int)(value * 1000));
            if (_navmeshAgent.isActiveAndEnabled)
            {
                if (_isDead)
                {
                    return;
                }
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