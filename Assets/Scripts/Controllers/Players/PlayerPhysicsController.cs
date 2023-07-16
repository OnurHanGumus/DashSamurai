using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;
using System.Threading.Tasks;
using Managers;

namespace Controllers {
    public class PlayerPhysicsController : MonoBehaviour, IAttackable
    {
        #region Self Variables

        #region Inject Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        public bool IsMoving { get => manager.IsMoving; }

        #endregion

        #region Serialized Variables
        [SerializeField] private PlayerManager manager;
        #endregion
        #region Private Variables
        private int _enemyHits = 2;
        private bool _isDeath = false;
        private int _health = 100;
        #endregion
        #endregion

        void IAttackable.OnWeaponTriggerEnter(int value)
        {
            if (_isDeath)
            {
                return;
            }

            else if (manager.IsMoving)
            {
                return;
            }

            _health -= value;

            if (_health <= 0)
            {
                _isDeath = true;
                PlayerSignals.onDied?.Invoke();
                CoreGameSignals.onLevelFailed?.Invoke();
                PlayerSignals.onChangeAnimation?.Invoke(PlayerAnimationStates.Die);

                DieDelay();
            }
            else
            {
                PlayerSignals.onHitted?.Invoke(_health - value);
            }
        }

        private async Task DieDelay()
        {
            await Task.Delay(1000);
        }
    }
}