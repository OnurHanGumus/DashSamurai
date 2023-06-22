using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Zenject;

public class HighScorePanelController : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private AudioSignals AudioSignals { get; set; }
    [Inject] private SaveSignals SaveSignals { get; set; }
    [Inject] private ScoreSignals ScoreSignals { get; set; }
    [Inject] private UISignals UISignals { get; set; }
    #endregion

    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI highScoreTxt;
    #endregion
    #endregion
    private void Start()
    {
        InitializeText();
    }

    private void InitializeText()
    {
        int score = SaveSignals.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
        highScoreTxt.text = score.ToString();
    }

    public void CloseScorePanel()
    {
        UISignals.onClosePanel?.Invoke(UIPanels.HighScorePanel);
        UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }

    public void OnUpdateText(int newValue)
    {
        highScoreTxt.text = newValue.ToString();
    }
}
