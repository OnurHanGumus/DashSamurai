using Components.Players;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "EnemySpawnSettings", menuName = "ZenjectExample/EnemySpawnSettings", order = 0)]
    public class EnemySpawnSettings : ScriptableObject
    {
        [SerializeField] public EnemySpawnManager.Settings EnemyManagerSpawnSettings;

    }
}