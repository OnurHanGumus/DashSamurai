using Events.External;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Enums;
using System;
using Data.MetaData;
using Components.Enemies;
using Signals;

namespace Controllers
{
    public class PlayerShootController : MonoBehaviour
    {
        [SerializeField] Vector3 playerCurrentPos;
        [SerializeField] private Transform bulletHolder;

        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private Settings _mySettings;

        private void Awake()
        {
            _mySettings = PlayerSettings.PlayerShootManagerSettings;
        }


        [Serializable]
        public class Settings
        {
            [SerializeField] public Vector3 ShootOffset;
        }
    }
}