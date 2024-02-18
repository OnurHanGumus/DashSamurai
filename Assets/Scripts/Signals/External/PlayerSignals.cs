using Components.Enemies;
using Enums;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;

[UsedImplicitly]
public class PlayerSignals
{
    public UnityAction onPlayerStopped = delegate { };

    public UnityAction<int> onHitted = delegate { };
    public UnityAction onDied = delegate { };
    public UnityAction<int> onUseAbility = delegate { };
    public UnityAction onLowStamina = delegate { };
    public UnityAction<int> onIncreaseSkill = delegate { };
    public UnityAction<PlayerAnimationStates> onChangeAnimation = delegate { };
    public UnityAction<PlayerAnimationStates> onResetTrigger = delegate { };

    public Func<Transform> onGetTransform = delegate { return null; };
    public Func<int> onGetStaminaValue = delegate { return 0; };
}