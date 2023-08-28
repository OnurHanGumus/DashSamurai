using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTypeSelector : ITypeSelector
{
    private List<Range> _rangeList;
    private EnemySpawnManager _spawnManager;
    private float _percentageIndeks = 0;
    private float _unitValue;
    private float _totalPercentages = 0f;
    private int _endValue;

    public EnemyTypeSelector(EnemySpawnManager spawnManager)
    {
        _spawnManager = spawnManager;
    }

    public void SetRange()
    {
        _percentageIndeks = 0;
        _unitValue = 0;
        _totalPercentages = 0f;
        _endValue = 0;
        _rangeList = new List<Range>();


        foreach (var i in _spawnManager.MySettings.Waves[_spawnManager.WaveId].SpawnableEnemies)
        {
            _totalPercentages += i.EnemyPercentage;
        }

        _unitValue = 100f / _totalPercentages;

        foreach (var i in _spawnManager.MySettings.Waves[_spawnManager.WaveId].SpawnableEnemies)
        {
            _endValue = (int)(_percentageIndeks + _unitValue * i.EnemyPercentage);
            _rangeList.Add(new Range((int)_percentageIndeks, _endValue));
            _percentageIndeks = _endValue;
        }

        #region Print
        //Debug.Log("list count: " + _rangeList.Count);
        //for (int i = 0; i < _rangeList.Count; i++)
        //{
        //    Debug.Log(_rangeList[i]);
        //}
        #endregion
    }

    public int GetType()
    {
        int rand = Random.Range(0, 100);

        for (int i = 0; i < _spawnManager.MySettings.Waves[_spawnManager.WaveId].SpawnableEnemies.Count; i++)
        {
            if (rand >= _rangeList[i].Start.Value && rand <= _rangeList[i].End.Value)
            {
                rand = i;
                break;
            }
        }

        if (rand > _spawnManager.MySettings.Waves[_spawnManager.WaveId].SpawnableEnemies.Count
            || rand > _rangeList[_rangeList.Count - 1].End.Value)
        {
            rand = 0;
        }

        return rand;
    }
}
