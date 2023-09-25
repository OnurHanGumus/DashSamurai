using Enums;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class AudioSignals
{
    public UnityAction<AudioSoundEnums> onPlaySound = delegate { };
}