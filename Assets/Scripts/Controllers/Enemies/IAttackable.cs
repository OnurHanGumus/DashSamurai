using UnityEngine.Events;

namespace Components.Enemies
{
    public interface IAttackable
    {
        void OnWeaponTriggerEnter(int value);
        EnemyInternalSignals GetInternalEvents();
    }
}