using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using Signals;
using DG.Tweening;

public class WavePanelController : MonoBehaviour
{
    [Inject] private ITimer WaveTimer { get; set; }
    [Inject] private LevelSignals WaveSignals { get; set; }
    [Inject] private CD_EnemySpawn SpawnSettings { get; set; }
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [SerializeField] private TextMeshProUGUI waveTimerText;

    private void Awake()
    {
        WaveTimer.onTimeUpdated += UpdateWaveTimerText;
        //CoreGameSignals.onPlay += OnWaveStarted;
    }

    private void UpdateWaveTimerText(int value)
    {
        waveTimerText.text = value.ToString();
    }

    //private void OnWaveStarted()
    //{
    //    WaveIdText.text = (0 + 1) + ". Wave";
    //    WaveIdText.DOFade(1, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
    //    {
    //        WaveIdText.DOFade(0, 1f).SetEase(Ease.Linear).SetDelay(SpawnSettings.WaveTextShowingTime);
    //    });
    //}
}
