using System;
using Data.MetaData;
using Events.External;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _myTransform;
        
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;

        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerCameraControllerSettings;
        }

        private void OnEnable()
        {
            RegisterEvents();
        }

        private void OnDisable()
        {
            UnRegisterEvents();
        }

        private void RegisterEvents()
        {
            PlayerSignals.onPlayerMove += OnPlayerMove;
            //MainSceneEvents.GameLoaded += OnGameLoaded;
        }

        //private void OnGameLoaded(){
        //loadPlayer
        //}
        
        private void OnPlayerMove(Vector3 playerPos)
        {
            _myTransform.position = playerPos + _mySettings.CameraOffset;
        }

        private void UnRegisterEvents()
        {
            PlayerSignals.onPlayerMove -= OnPlayerMove;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 CameraOffset;
        }
    }
}
