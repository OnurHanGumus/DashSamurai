using Enums;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class AbilitySignals
{
    public UnityAction<CollectableEnums> onActivateAbility = delegate { };
}