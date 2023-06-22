using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;
using Enums;
using Zenject;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        [Inject] private UISignals UISignals { get; set; }
        [Inject] private ScoreSignals ScoreSignals { get; set; }
        #endregion

        #region Public Variables


        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables
        private ScoreData _data;
        private int _gem;
        public int Gem
        {
            get { return _gem; }
            set
            {
                _gem = value;
                UISignals.onSetChangedText?.Invoke(ScoreTypeEnums.Gem, Gem);
            }
        }


        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            Gem = SaveSignals.onGetScore(SaveLoadStates.Gem, SaveFiles.SaveFile);
        }
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ScoreSignals.onScoreIncrease += OnScoreIncrease;
            ScoreSignals.onScoreDecrease += OnScoreDecrease;
            ScoreSignals.onGetGem += OnGetGem;
            CoreGameSignals.onNextLevel += OnNextLevel;
            CoreGameSignals.onRestart += OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.onScoreIncrease -= OnScoreIncrease;
            ScoreSignals.onScoreDecrease -= OnScoreDecrease;
            ScoreSignals.onGetGem -= OnGetGem;
            CoreGameSignals.onNextLevel -= OnNextLevel;
            CoreGameSignals.onRestart -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnScoreIncrease(ScoreTypeEnums type, int amount)
        {
            Gem += amount;
        }

        private void OnScoreDecrease(ScoreTypeEnums type, int amount)
        {
            Gem -= amount;
            SaveSignals.onSave(Gem, SaveLoadStates.Gem, SaveFiles.SaveFile);
        }

        private void OnNextLevel()
        {
            SaveSignals.onSave(Gem, SaveLoadStates.Gem, SaveFiles.SaveFile);
        }
        private int OnGetGem()
        {
            return Gem;
        }

        private void OnRestartLevel()
        {
        }
    }
}