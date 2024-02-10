using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameOverPanelController : MonoBehaviour
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
    [SerializeField] private GameObject successPanel, failPanel;
    [SerializeField] private TextMeshProUGUI scoreTxt;

    #endregion
    #region Private Variables
    private int _highScore;
    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _highScore = InitializeHighScore();
    }

    private void Start()
    {

    }

    public void CloseGameOverPanel()
    {
        UISignals.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    private int InitializeHighScore()
    {
        return SaveSignals.onGetScore(SaveLoadStates.Score, SaveFiles.SaveFile);
    }
    public void ShowThePanel()
    {
        int temp = ScoreSignals.onGetScore();

        if(temp > _highScore)
        {
            successPanel.SetActive(true);
            failPanel.SetActive(false);
            scoreTxt.text = "High Score: " + temp;
            _highScore = temp;
            SaveSignals.onSave?.Invoke(temp,SaveLoadStates.Score,SaveFiles.SaveFile);
        }
        else
        {
            successPanel.SetActive(false);
            failPanel.SetActive(true);
            scoreTxt.text = "Score: " + temp;
        }
    }

    public void TryAgainBtn()
    {
        CoreGameSignals.onRestart?.Invoke();
        UISignals.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        CoreGameSignals.onPlay?.Invoke();
    }
    public void MenuBtn()
    {
        CoreGameSignals.onRestart?.Invoke();
        UISignals.onClosePanel?.Invoke(UIPanels.GameOverPanel);
        UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
    }
    public void Open()
    {
        CoreGameSignals.onLevelFailed?.Invoke();
        AudioSignals.onPlaySound(AudioSoundEnums.Loose);
    }
}
