using Controllers;
using Enums;
using Signals;
using UnityEngine;
using Data.UnityObject;
using Data.ValueObject;
using Zenject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private AudioSignals AudioSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        [Inject] private ScoreSignals ScoreSignals { get; set; }
        [Inject] private UISignals UISignals { get; set; }
        #endregion

        #region Serialized Variables

        [SerializeField] private UIPanelActivenessController uiPanelController;
        [SerializeField] private GameOverPanelController gameOverPanelController;
        [SerializeField] private LevelPanelController levelPanelController;
        [SerializeField] private HighScorePanelController highScorePanelController;

        #endregion
        #region Private Variables
        private UIData _data;
        private bool _isStorePanelOpened = false;

        #endregion
        #endregion

        #region Event Subscriptions
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }

        private UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.onOpenPanel += OnOpenPanel;
            UISignals.onClosePanel += OnClosePanel;
            UISignals.onSetChangedText += levelPanelController.OnScoreUpdateText;
            CoreGameSignals.onPlay += OnPlay;
            CoreGameSignals.onLevelFailed += OnLevelFailed;
            CoreGameSignals.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.onRestart += levelPanelController.OnRestartLevel;
            ScoreSignals.onHighScoreChanged += highScorePanelController.OnUpdateText;
        }

        private void UnsubscribeEvents()
        {
            UISignals.onOpenPanel -= OnOpenPanel;
            UISignals.onClosePanel -= OnClosePanel;
            UISignals.onSetChangedText -= levelPanelController.OnScoreUpdateText;
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.onRestart -= levelPanelController.OnRestartLevel;
            ScoreSignals.onHighScoreChanged -= highScorePanelController.OnUpdateText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenMenu(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.CloseMenu(panelParam);
        }

        private void OnPlay()
        {
            UISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.FailPanel);
            //gameOverPanelController.ShowThePanel();
        }

        private void OnLevelSuccessful()
        {
            UISignals.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.WinPanel);
        }

        public void Play()
        {
            CoreGameSignals.onPlay?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
        }

        public void NextLevel()
        {
            CoreGameSignals.onNextLevel?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            CoreGameSignals.onRestart?.Invoke();
            UISignals.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void PauseButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.PausePanel);
            Time.timeScale = 0f;
        }
        public void HighScoreButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.HighScorePanel);
            UISignals.onClosePanel?.Invoke(UIPanels.StartPanel);
        }
        public void OptionsButton()
        {
            UISignals.onOpenPanel?.Invoke(UIPanels.OptionsPanel);
            Time.timeScale = 0f;
            //Debug.Log("Clicked");
        }

        public void StoreButton()
        {
            if (!_isStorePanelOpened)
            {
                UISignals.onOpenPanel?.Invoke(UIPanels.StorePanel);
            }
            else
            {
                UISignals.onClosePanel?.Invoke(UIPanels.StorePanel);
            }
            _isStorePanelOpened = !_isStorePanelOpened;

        }
    }
}