using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Signals;
using System.Threading.Tasks;
using Components.Enemies;
using Data.MetaData;

namespace Controllers
{
    public class EnemyPhysicsController : MonoBehaviour, IAttackable
    {
        #region Self Variables

        #region Inject Variables
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
        [Inject] private EnemySettings _mySettings;
        public bool IsMoving { get => false; }
        public bool IsHitted { get; set; }
        public bool IsDeath { get; set; }

        #endregion

        #region Serialized Variables
        [SerializeField] private GameObject enemy;


        #endregion
        #region Private Variables
        private int _enemyHits;
        private int _randomHittedAnimId = 0;
        #endregion
        #endregion

        private void OnEnable()
        {
            IsDeath = false;
            _enemyHits = _mySettings.Health;
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
                LevelSignals.onEnemyDied?.Invoke();
                //LevelSignals.onEnemyDied.Invoke();

                DieDelay();
            }
            else
            {
                IsHitted = true;
            }
        }

        private async Task DieDelay()
        {
            await Task.Delay(System.TimeSpan.FromSeconds(_mySettings.DeathDuration));
            enemy.SetActive(false);
        }
    }
}