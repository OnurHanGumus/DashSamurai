using Components.Enemies;
using Enums;
using System.Threading.Tasks;
using UnityEngine.Events;

public class EnemyInternalSignals
{
    public UnityAction<IAttackable> onDeath = delegate { };
    public UnityAction onDisabled = delegate { };
    public UnityAction onHitted = delegate { };
    public UnityAction onAttack = delegate { };
    public UnityAction<EnemyAnimationStates> onChangeAnimation = delegate { };
    public UnityAction<EnemyAnimationStates> onResetAnimation = delegate { };
    public UnityAction<EnemyStateEnums> onChangeState = delegate { };
}