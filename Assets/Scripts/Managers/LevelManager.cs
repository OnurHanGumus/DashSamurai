using System;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Events.External;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        #endregion
        #region Public Variables


        #endregion

        #region Serialized Variables

        [Space] [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand levelClearer;

        #endregion

        #region Private Variables

        private int _levelID;
        private LevelData _data;
        private int _currentModdedLevel = 0;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _levelID = GetActiveLevel();
            _data = GetData();
        }
        private LevelData GetData() => Resources.Load<CD_Level>("Data/CD_Level").Data;
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }


        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onLevelInitialize += OnInitializeLevel;
            CoreGameSignals.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.onNextLevel += OnNextLevel;
            CoreGameSignals.onRestart += OnRestartLevel;
            LevelSignals.onGetLevelId += OnGetLevelId;
            LevelSignals.onGetCurrentModdedLevel += OnGetModdedLevel;

        }



        private void UnsubscribeEvents()
        {
            CoreGameSignals.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.onNextLevel -= OnNextLevel;
            CoreGameSignals.onRestart -= OnRestartLevel;
            LevelSignals.onGetLevelId -= OnGetLevelId;
            LevelSignals.onGetCurrentModdedLevel -= OnGetModdedLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            OnInitializeLevel();
        }

        private void OnNextLevel()
        {
            _levelID++;
            CoreGameSignals.onClearActiveLevel?.Invoke();
            CoreGameSignals.onRestart?.Invoke();
            SaveSignals.onSave(_levelID, SaveLoadStates.Level, SaveFiles.SaveFile);

        }

        private void OnRestartLevel()
        {
            CoreGameSignals.onClearActiveLevel?.Invoke();
            CoreGameSignals.onReset?.Invoke();
            CoreGameSignals.onLevelInitialize?.Invoke();
        }

        private int OnGetLevelId()
        {
            return _levelID;
        }


        private void OnInitializeLevel()
        {
            UnityEngine.Object[] Levels = Resources.LoadAll("Levels");
            int newLevelId = _levelID % Levels.Length;
            _currentModdedLevel = newLevelId;
            levelLoader.InitializeLevel((GameObject)Levels[newLevelId], levelHolder.transform);
        }

        private int OnGetModdedLevel()
        {
            return _currentModdedLevel;
        }

        private void OnClearActiveLevel()
        {
            levelClearer.ClearActiveLevel(levelHolder.transform);
        }
    }
}