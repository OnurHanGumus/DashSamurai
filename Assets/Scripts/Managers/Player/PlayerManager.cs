using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        [Inject] private LevelSignals LevelSignals { get; set; }
        #endregion

        #region Public Variables
        public bool IsMoving = false;
        #endregion

        #region Serialized Variables
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimationController animationController;

        [SerializeField] private GameObject playerMesh;
        [SerializeField] private Vector3 rigPosition;
        [SerializeField] private Transform rig;

        [SerializeField] private Vector3 playerInitializePosition;

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
            CoreGameSignals.onPlay += OnPlay;
            CoreGameSignals.onRestart += movementController.OnRestartLevel;
            CoreGameSignals.onRestart += physicsController.OnRestart;

            InputSignals.onInputDragged += movementController.OnInputDragged;

            PlayerSignals.onPlayerStopped += movementController.OnPlayerStopped;
            PlayerSignals.onChangeAnimation += animationController.OnChangeAnimation;
            PlayerSignals.onResetTrigger += animationController.OnResetTrigger;
            PlayerSignals.onGetTransform += OnGetTransform;
            PlayerSignals.onDied += OnDie;

            CoreGameSignals.onNextLevel += OnNextLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.onPlay -= movementController.OnPlay;
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onRestart -= movementController.OnRestartLevel;
            CoreGameSignals.onRestart -= physicsController.OnRestart;

            InputSignals.onInputDragged -= movementController.OnInputDragged;

            PlayerSignals.onPlayerStopped -= movementController.OnPlayerStopped;
            PlayerSignals.onChangeAnimation -= animationController.OnChangeAnimation;
            PlayerSignals.onResetTrigger -= animationController.OnResetTrigger;
            PlayerSignals.onGetTransform -= OnGetTransform;
            PlayerSignals.onDied -= OnDie;

            CoreGameSignals.onNextLevel -= OnNextLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnPlay()
        {
            playerMesh.SetActive(true);
        }

        private void OnDie()
        {
            Deactive();
        }

        private async Task Deactive()
        {
            await Task.Delay(TimeSpan.FromSeconds(1f));
            playerMesh.SetActive(false);
            rig.localPosition = rigPosition;
        }

        private Transform OnGetTransform()
        {
            return transform;
        }

        private void OnNextLevel()
        {
            transform.position = playerInitializePosition;
        }
    }
}