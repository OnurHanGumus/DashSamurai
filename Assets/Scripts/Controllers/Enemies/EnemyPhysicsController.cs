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
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }

        public bool IsMoving { get => false; }
        #endregion

        #region Serialized Variables
        [SerializeField] private EnemyManager2 enemy;


        #endregion
        #region Private Variables
        private int _enemyHits = 2;
        private int _randomHittedAnimId = 0;
        private bool _isDeath = false;
        #endregion
        #endregion

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
                EnemyInternalSignals.onDeath?.Invoke(this);
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
            await Task.Delay(System.TimeSpan.FromSeconds(1));
            enemy.gameObject.SetActive(false);
        }
    }
}