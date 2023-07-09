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
        #region Self Variables

        #region Inject Variables
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalEvents { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
        #endregion

        #region Serialized Variables
        [SerializeField] private EnemyManager enemy;


        #endregion
        #region Private Variables
        private int _enemyHits = 2;
        private int _randomHittedAnimId = 0;
        private bool _isDeath = false;
        #endregion
        #endregion

        public EnemyInternalSignals GetInternalEvents()
        {
            return EnemyInternalEvents;
        }

        private void OnEnable()
        {
            _isDeath = false;
        }

        private void OnDisable()
        {
            _enemyHits = 2;
        }

        void IAttackable.OnWeaponTriggerEnter(int value)
        {
            if (_isDeath)
            {
                return;
            }

            _enemyHits -= value;

            if (_enemyHits <= 0)
            {
                _isDeath = true;
                EnemyInternalEvents.onDeath?.Invoke(this);
                LevelSignals.onEnemyDied.Invoke();
                EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Die);
                DieDelay();
            }
            else
            {
                _randomHittedAnimId = Random.Range(0, 2);


                EnemyInternalSignals.onHitted?.Invoke();
                EnemyInternalSignals.onResetAnimation?.Invoke(EnemyAnimationStates.Attack1);
                EnemyInternalSignals.onChangeAnimation?.Invoke((EnemyAnimationStates) _randomHittedAnimId);
            }
        }

        private async Task DieDelay()
        {
            await Task.Delay(1000);
            enemy.gameObject.SetActive(false);
        }
    }
}