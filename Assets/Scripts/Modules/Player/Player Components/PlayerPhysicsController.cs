using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Enums;
using Signals;
using Components.Enemies;
using System.Threading.Tasks;
using Managers;

public class PlayerPhysicsController : MonoBehaviour, IAttackable
{
    #region Self Variables

    #region Inject Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private AbilitySettings AbilitySettings { get; set; }
    public bool IsMoving { get => manager.IsMoving; }
    public int Health { get; set; } = 100;

    #endregion

    #region Serialized Variables
    [SerializeField] private PlayerManager manager;
    #endregion
    #region Private Variables
    private bool _isDeath = false;
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

        Health -= value * (AbilitySettings.AbilityDatas[(int)CollectableEnums.Shield].IsActivated ? 0:1);
        PlayerSignals.onHitted?.Invoke(Health);

        if (Health <= 0)
        {
            _isDeath = true;
            PlayerSignals.onDied?.Invoke();
            CoreGameSignals.onLevelFailed?.Invoke();
            PlayerSignals.onChangeAnimation?.Invoke(PlayerAnimationStates.Die);

            DieDelay();
        }
    }

    public void OnRestart()
    {
        Health = 100;
        PlayerSignals.onHitted?.Invoke(Health);
        _isDeath = false;
    }

    private async Task DieDelay()
    {
        await Task.Delay(1000);
    }
}