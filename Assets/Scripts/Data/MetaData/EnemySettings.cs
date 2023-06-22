using Components.Players;
using Components.Enemies;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] public EnemyMovementController.Settings EnemyMovementSettings;
    }
}