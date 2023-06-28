using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;
using System.Threading.Tasks;

namespace Controllers {
    public class EnemyPhysicsController : MonoBehaviour, IAttackable
    {
        private int _enemyHits = 2;
        [Inject] private LevelSignals LevelSignals { get; set;}
        [Inject] private EnemyInternalSignals EnemyInternalEvents { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
        [SerializeField] private EnemyManager enemy;

        public EnemyInternalSignals GetInternalEvents()
        {
            return EnemyInternalEvents;
        }

        private void OnDisable()
        {
            _enemyHits = 2;
        }

        void IAttackable.OnWeaponTriggerEnter(int value)
        {
            _enemyHits -= value;

            if (_enemyHits == 0)
            {
                EnemyInternalEvents.onDeath?.Invoke(this);
                LevelSignals.onEnemyDied.Invoke();
                EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Die);
                //PoolSignals.onRemove?.Invoke(PoolEnums.Enemy, enemy);
                DieDelay();

            }
        }

        private async Task DieDelay()
        {
            await Task.Delay(1000);
            enemy.gameObject.SetActive(false);
        }
    }
}