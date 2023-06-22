using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestart = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };

        public readonly struct InputUpdate
        {
            public readonly Vector3 TerrainPos;
        
            public InputUpdate(Vector3 terrainPos) {
                TerrainPos = terrainPos;
            }
        }
    }
}