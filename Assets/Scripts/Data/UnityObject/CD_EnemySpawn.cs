using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_EnemySpawn", menuName = "ZenjectExample/CD_EnemySpawn", order = 0)]
public class CD_EnemySpawn : ScriptableObject
{
    //[SerializeField] public EnemySpawnManager.Settings EnemyManagerSpawnSettings;
    public List<WaveData> Waves;

    public float TimeBetweenWaves = 1f;
    public float WaveTextShowingTime = 1f;
}