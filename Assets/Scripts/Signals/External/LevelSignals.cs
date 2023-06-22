using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals
    {
        public Func<int> onGetCurrentModdedLevel = delegate { return 0; };
        public Func<int> onGetLevelId = delegate { return 0; };
        public UnityAction onEnemyDied = delegate { };

    }
}