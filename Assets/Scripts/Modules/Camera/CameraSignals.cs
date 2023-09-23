using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSignals
{
    public UnityAction<CameraStatesEnum> onChangeState = delegate { };
    public UnityAction<Transform> onSetTarget = delegate { };
}