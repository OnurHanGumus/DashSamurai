using System;
using System.Collections.Generic;
using Commands;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables
        #region Injected Variables
        [Inject] private PoolSignals PoolSignals { get; set; }
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        #endregion

        #region Public Variables

        #endregion

        #region Serialized Variables
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerShootController shootController;
        #endregion

        #region Private Variables
        private PlayerData _data;
        #endregion

        #endregion

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _data = GetData();
        }
        public PlayerData GetData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.onPlay += movementController.OnPlay;
            CoreGameSignals.onRestart += movementController.OnRestartLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onPlay -= movementController.OnPlay;
            CoreGameSignals.onRestart -= movementController.OnRestartLevel;
        }


        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private void OnPlay()
        {

        }
        private void OnResetLevel()
        {

        }
    }
}