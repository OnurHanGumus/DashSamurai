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
        public bool IsHitted { get; set; }
        public bool IsDeath { get; set; }

        #endregion

        #region Serialized Variables
        [SerializeField] private EnemyManager2 enemy;


        #endregion
        #region Private Variables
        private int _enemyHits = 100;
        private int _randomHittedAnimId = 0;
        #endregion
        #endregion

        private void OnEnable()
        {
            IsDeath = false;
        }

        private void OnDisable()
        {
            _enemyHits = 2;
        }

        void IAttackable.OnWeaponTriggerEnter(int value)
        {
            if (IsDeath)
            {
                return;
            }

            _enemyHits -= value;

            if (_enemyHits <= 0)
            {
                IsDeath = true;
                EnemyInternalSignals.onDeath?.Invoke(this);
                LevelSignals.onEnemyDied.Invoke();
                EnemyInternalSignals.onChangeAnimation?.Invoke(EnemyAnimationStates.Die);
                DieDelay();
            }
            else
            {
                _randomHittedAnimId = Random.Range(0, 1);

                IsHitted = true;
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