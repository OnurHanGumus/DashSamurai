using Components.Players;
using Components.Enemies;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySettings", menuName = "ZenjectExample/EnemySettings", order = 0)]
public class EnemySettings : ScriptableObject
{
    [Header("Timings")]
    [SerializeField] public float AttackDelay = 2.5f;
    [SerializeField] public float AttackRotatableTime = 1.2f;
    [SerializeField] public float AnyStateDuration = 1f;
    [SerializeField] public float DeathDuration = 1f;

    [Header("Values")]
    [SerializeField] public int AttackPower = 10;
    [SerializeField] public int Health = 4;
    [SerializeField] public float MoveSpeed = 1f;

    [Header("Distances")]
    [SerializeField] public float AttackDistance = 1f;
    [SerializeField] public float AttackToMoveDistance = 1.5f;

}