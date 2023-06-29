using Components.Enemies;
using Enums;
using UnityEngine.Events;

public class EnemyInternalSignals
{
    public UnityAction<IAttackable> onDeath = delegate { };
    public UnityAction onHitted = delegate { };
    public UnityAction<EnemyAnimationStates> onChangeAnimation = delegate { };
}