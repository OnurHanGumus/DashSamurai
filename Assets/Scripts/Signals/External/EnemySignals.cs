using Components.Enemies;
using Enums;
using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.Events;

[UsedImplicitly]
public class EnemySignals
{
    public UnityAction<EnemyAnimationStates> onChangeAnimation = delegate { };
}