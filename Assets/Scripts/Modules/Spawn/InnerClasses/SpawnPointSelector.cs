using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnPointSelector : IPointSelector
{
    private Transform[] _spawnPoints;
    public SpawnPointSelector(params Transform[] spawnPoints)
    {
        _spawnPoints = spawnPoints;
    }

    #region Obsolete GetPoint Methods

    public Vector3 GetRandomPoint(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private Vector3 GetRandomOnCirclePoint(float radius)
    {
        float angle = radius * Random.Range(0, 360);

        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector3(x, 0.0f, z);
    }

    private Vector3 GetRandomSpawner()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }
    #endregion

    private Vector3 GetRandomInCirclePoint(float radius)
    {
        radius = Random.Range(0f, radius);
        float angle = radius * Random.Range(0, 360);

        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector3(x, 0.0f, z);
    }

    private Vector3 GetRandomPositionFromRandomSpawner(float radius)
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position + GetRandomInCirclePoint(radius);
    }

    public Vector3 GetPoint(float radius)
    {
        return GetRandomPoint(radius);
    }
}
