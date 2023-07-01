using System;
using System.Collections.Generic;
using Commands;
using Signals;
using UnityEngine;
using Enums;
using Zenject;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private SaveSignals SaveSignals { get; set; }
        #endregion
        #region Private Variables

        private LoadGameDataCommand LoadGameDataCommand;
        private SaveGameCommand _saveGameCommand;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            LoadGameDataCommand = new LoadGameDataCommand();
            _saveGameCommand = new SaveGameCommand();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            SaveSignals.onSave += OnSaveData;
            SaveSignals.onChangeSoundState += OnSaveData;
            SaveSignals.onGetScore += OnGetData;
            SaveSignals.onGetSoundState += OnGetData;
            SaveSignals.onBuyItem += OnSaveList;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.onSave -= OnSaveData;
            SaveSignals.onChangeSoundState -= OnSaveData;
            SaveSignals.onGetScore -= OnGetData;
            SaveSignals.onGetSoundState -= OnGetData;
            SaveSignals.onBuyItem -= OnSaveList;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
            SendData();
        }

        private void OnSaveData(int value, SaveLoadStates saveType, SaveFiles saveFiles)
        {
            _saveGameCommand.OnSaveData(saveType, value, saveFiles.ToString());
        }

        private int OnGetData(SaveLoadStates state, SaveFiles file)
        {
            return LoadGameDataCommand.OnLoadGameData(state, file.ToString());
        }

        private void OnSaveList(List<int> newList, SaveLoadStates saveType, SaveFiles saveFiles)
        {
            _saveGameCommand.OnSaveList(saveType, newList, saveFiles.ToString());
            SendData();
        }

        private List<int> OnGetList(SaveLoadStates saveType, SaveFiles saveFiles)
        {
            return LoadGameDataCommand.OnLoadGameList(saveType, saveFiles.ToString());
        }

        private void SendData()
        {
            SaveSignals.onInitializeBuyedItems?.Invoke(OnGetList(SaveLoadStates.BuyItem, SaveFiles.SaveFile));
        }
    }
}