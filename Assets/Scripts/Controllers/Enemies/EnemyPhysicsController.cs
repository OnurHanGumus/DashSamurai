using Events.External;
using Events.InternalEvents;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;

namespace Controllers {
    public class EnemyPhysicsController : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        [Inject] private LevelSignals LevelSignals { get; set;}
        [Inject] private EnemyInternalEvents EnemyInternalEvents { get; set; }
        [SerializeField] private EnemyManager enemy;

        public EnemyInternalEvents GetInternalEvents()
        {
            return EnemyInternalEvents;
        }

        private void OnDisable()
        {
            _enemyHits = 2;
        }
        void IAttackable.OnWeaponTriggerEnter()
        {
            _enemyHits--;

            if (_enemyHits == 0)
            {
                EnemyInternalEvents.OnDeath?.Invoke(this);
                LevelSignals.onEnemyDied.Invoke();
                //PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, enemy);

                enemy.gameObject.SetActive(false);
            }
        }
    }
}