using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WaveData
{
    [System.Serializable]
    public struct Enemies
    {
        public EnemyTypeEnums EnemyType;
        public float EnemyPercentage;
    }

    [System.Serializable]
    public struct Collectables
    {
        public CollectableEnums CollectableType;
        public int SecondToInstantiate;
        public Vector3 Position;
    }

    [System.Serializable]
    public struct Bosses
    {
        public BossTypeEnums BossType;
        public int SecondToInstantiate;
        public Vector3 Position;
    }

    public List<Enemies> SpawnableEnemies;
    public List<Collectables> SpawnableCollectables;
    public List<Bosses> SpawnBosses;
    public AnimationCurve SpawnDelay;
    public int WaveDuration;
    public float WaveScale;
}