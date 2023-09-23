using System;
using System.Collections;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Zenject;
//using UnityEditor.AI;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private LevelSignals LevelSignals { get; set; }
        [Inject] private SaveSignals SaveSignals { get; set; }
        [Inject] private CameraSignals CameraSignals { get; set; }
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
        UnityEngine.Object[] _levels;
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
            _levels = Resources.LoadAll("Levels");
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
            CoreGameSignals.onPlay += OnPlay;
            LevelSignals.onGetLevelId += OnGetLevelId;
            LevelSignals.onGetCurrentModdedLevel += OnGetModdedLevel;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onLevelInitialize -= OnInitializeLevel;
            CoreGameSignals.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.onNextLevel -= OnNextLevel;
            CoreGameSignals.onRestart -= OnRestartLevel;
            CoreGameSignals.onPlay -= OnPlay;
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

        private void OnPlay()
        {
            RebindNavmesh();
        }

        private void OnNextLevel()
        {
            CameraSignals.onChangeState?.Invoke(CameraStatesEnum.TransitionPre);
            StartCoroutine(NextLevelDelay());

        }

        IEnumerator NextLevelDelay()
        {
            yield return new WaitForSeconds(2f);

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
            int newLevelId = _levelID % _levels.Length;
            _currentModdedLevel = newLevelId;
            levelLoader.InitializeLevel((GameObject)_levels[newLevelId], levelHolder.transform);

            CameraSignals.onChangeState?.Invoke(CameraStatesEnum.Init);

        }

        private void RebindNavmesh()
        {

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