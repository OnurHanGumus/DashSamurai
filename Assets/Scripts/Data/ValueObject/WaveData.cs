using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WaveData
{
    [System.Serializable]
    public struct Enemies
    {
        public EnemyTypeEnums enemyType;
        public float EnemyPercentage;
    }
    public List<Enemies> SpawnableEnemies;
    public AnimationCurve SpawnDelay;
    public int WaveDuration;
    public float WaveScale;
}