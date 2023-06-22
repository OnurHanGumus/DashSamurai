using Components.Enemies;
using UnityEngine.Events;

namespace Events.InternalEvents
{
    public class EnemyInternalEvents
    {
        public UnityAction<IAttackable> OnDeath = delegate { };
    }
}