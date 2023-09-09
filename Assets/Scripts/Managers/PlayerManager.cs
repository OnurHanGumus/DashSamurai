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
        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        #endregion

        #region Public Variables
        public bool IsMoving = false;
        #endregion

        #region Serialized Variables
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimationController animationController;
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
            CoreGameSignals.onRestart += physicsController.OnRestart;

            InputSignals.onInputDragged += movementController.OnInputDragged;

            PlayerSignals.onPlayerStopped += movementController.OnPlayerStopped;
            PlayerSignals.onChangeAnimation += animationController.OnChangeAnimation;
            PlayerSignals.onResetTrigger += animationController.OnResetTrigger;
            PlayerSignals.onGetTransform += OnGetTransform;

        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onPlay -= movementController.OnPlay;
            CoreGameSignals.onRestart -= movementController.OnRestartLevel;
            CoreGameSignals.onRestart -= physicsController.OnRestart;

            InputSignals.onInputDragged -= movementController.OnInputDragged;

            PlayerSignals.onPlayerStopped -= movementController.OnPlayerStopped;
            PlayerSignals.onChangeAnimation -= animationController.OnChangeAnimation;
            PlayerSignals.onResetTrigger -= animationController.OnResetTrigger;
            PlayerSignals.onGetTransform -= OnGetTransform;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private Transform OnGetTransform()
        {
            return transform;
        }
    }
}