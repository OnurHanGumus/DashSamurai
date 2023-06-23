using Components.Enemies;
using Enums;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[UsedImplicitly]
public class PlayerSignals
{
    public UnityAction onPlayerStopped = delegate { };
    public UnityAction<PlayerAnimationStates> onChangeAnimation = delegate { };
}