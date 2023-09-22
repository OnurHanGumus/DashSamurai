using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using Signals;
using DG.Tweening;

public class WavePanelController : MonoBehaviour
{
    [Inject] private ITimer _waveTimer { get; set; }
    [Inject] private LevelSignals _levelSignals { get; set; }
    [Inject] private CD_EnemySpawn _spawnSettings { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }
    [SerializeField] private TextMeshProUGUI waveTimerText, levelText;

    private void Awake()
    {
        _waveTimer.onTimeUpdated += UpdateWaveTimerText;
        _coreGameSignals.onPlay += OnWaveStarted;
    }

    private void UpdateWaveTimerText(int value)
    {
        waveTimerText.text = value.ToString();
    }

    private void OnWaveStarted()
    {
        int id = _levelSignals.onGetLevelId();
        levelText.text = "Level: " + (id + 1);
        //levelText.DOFade(1, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
        //{
        //    levelText.DOFade(0, 1f).SetEase(Ease.Linear).SetDelay(_spawnSettings.WaveTextShowingTime);
        //});
    }
}
