using Components.Players;
using Components.Enemies;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
public class EnemySettings : ScriptableObject
{
    [SerializeField] public float AttackDelay = 2.5f;
    [SerializeField] public float AttackRotatableTime = 1.2f;

    [SerializeField] public float AttackDistance = 1f;
    [SerializeField] public float AttackToMoveDistance = 1.5f;
    [SerializeField] public float AnyStateExitDelay = 1f;

    [SerializeField] public float MoveSpeed = 1f;
    [SerializeField] public int Health = 4;
}