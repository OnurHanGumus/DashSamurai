using Data.MetaData;
using Managers;
using Signals;
using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        [Inject] private PlayerSettings PlayerSettings { get; set; }

        private RoutineHelper _onPosUpdate;
        private Settings _mySettings;
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private bool isPlayerStopped = true;

        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private Vector3 _input = new Vector3();

        private bool _isNotStarted = true;

        #endregion
        #endregion


        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rig = GetComponent<Rigidbody>();
            _manager = GetComponent<PlayerManager>();
            _mySettings = PlayerSettings.PlayerMovementSettings;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (_isNotStarted)
            {
                return;
            }

            _rig.velocity = _input * _mySettings.Speed * (isPlayerStopped? 0:1);
        }

        public void OnPlay()
        {
            _isNotStarted = false;
        }

        public void OnInputDragged(Vector3 input)
        {
            if (isPlayerStopped == false)
            {
                return;
            }

            if (new Vector2(input.x, input.z).magnitude < 0.5f)
            {
                return;
            }

            isPlayerStopped = false;
            _input = input;

            if (Mathf.Abs(input.x) >= Mathf.Abs(input.z))
            {
                _input.z = 0;
            }
            else if (Mathf.Abs(input.x) < Mathf.Abs(input.z))
            {
                _input.x = 0;
            }
            transform.LookAt(transform.localPosition + _input);

            Debug.Log(_input);
        }

        public void OnPlayerStopped()
        {
            isPlayerStopped = true;
        }

        public void OnRestartLevel()
        {
            _isNotStarted = true;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
        }
    }
}