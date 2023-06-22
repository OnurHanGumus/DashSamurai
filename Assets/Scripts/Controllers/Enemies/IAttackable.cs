using Events.InternalEvents;
using UnityEngine.Events;

namespace Components.Enemies
{
    public interface IAttackable
    {
        void OnWeaponTriggerEnter();
        EnemyInternalEvents GetInternalEvents();
    }
}