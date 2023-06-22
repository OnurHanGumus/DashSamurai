using Components.Players;
using UnityEngine;

namespace Data.MetaData
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "ZenjectExample/BulletSettings", order = 0)]
    public class BulletSettings : ScriptableObject
    {
        [SerializeField] public BulletPhysicsController.Settings BulletCollisionDetectorSettings;
    }
}