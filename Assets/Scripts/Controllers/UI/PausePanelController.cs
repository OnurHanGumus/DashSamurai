using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PausePanelController : MonoBehaviour
{
    #region Injected Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private AudioSignals AudioSignals { get; set; }
    [Inject] private SaveSignals SaveSignals { get; set; }
    [Inject] private ScoreSignals ScoreSignals { get; set; }
    [Inject] private UISignals UISignals { get; set; }
    #endregion

    public void ClosePausePanel()
    {
        UISignals.onClosePanel?.Invoke(UIPanels.PausePanel);
        Time.timeScale = 1f;
    }
    public void MainMenuBtn()
    {
        UISignals.onClosePanel?.Invoke(UIPanels.PausePanel);
        UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
        Time.timeScale = 1f;

    }
    public void ExitBtn()
    {
        Application.Quit();
    }


}
