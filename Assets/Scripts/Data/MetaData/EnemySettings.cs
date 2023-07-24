using Components.Players;
using Components.Enemies;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] public float AttackDelay = 2.5f;
        [SerializeField] public float AttackRotatableTime = 1.2f;

        [SerializeField] public float AttackDistance = 1f;
        [SerializeField] public float AnyStateExitDelay = 1f;
    }
}