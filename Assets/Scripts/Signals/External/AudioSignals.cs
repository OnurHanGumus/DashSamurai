using Enums;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Signals
{
    public class AudioSignals
    {
        public UnityAction<AudioSoundEnums> onPlaySound = delegate { };
    }
}