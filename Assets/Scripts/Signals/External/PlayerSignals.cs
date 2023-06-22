using Components.Enemies;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace Events.External
{
    [UsedImplicitly]
    public class PlayerSignals
    {
        public UnityAction<Vector3> onPlayerMove;
        public UnityAction<IAttackable> onEnemyShooted;
        public UnityAction<Vector3> onAttackedToEnemy;

    }
}