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
        [Inject] private PlayerSignals PlayerSignals { get; set; }

        private Settings _mySettings;
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private bool isPlayerStopped = true;
        [SerializeField] private PlayerGroundDetector groundDetector;

        #endregion
        #region Private Variables
        private Rigidbody _rig;
        private PlayerManager _manager;
        private Vector3 _input = new Vector3();
        private Vector3 _pastInput = new Vector3();

        private bool _isNotStarted = true;
        private float _currentTime = 0f;

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
            _currentTime += Time.deltaTime;

            Move();
        }

        private void Move()
        {
            if (_isNotStarted)
            {
                return;
            }

            _rig.velocity = _input * _mySettings.Speed *_mySettings.SpeedCurve.Evaluate(_currentTime) * (isPlayerStopped? 0:1);
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

            _input = input;

            if (Mathf.Abs(input.x) >= Mathf.Abs(input.z))
            {
                _input.z = 0;
            }
            else if (Mathf.Abs(input.x) < Mathf.Abs(input.z))
            {
                _input.x = 0;
            }

            if (_input.Equals(_pastInput))
            {
                return;
            }

            _pastInput = _input;
            transform.LookAt(transform.localPosition + _input);

            PlayerSignals.onChangeAnimation?.Invoke(Enums.PlayerAnimationStates.Move);
            _currentTime = 0f;
            isPlayerStopped = false;
            _manager.IsMoving = true;
        }

        public void OnPlayerStopped()
        {
            //PlayerSignals.onResetTrigger.Invoke(Enums.PlayerAnimationStates.Move);
            isPlayerStopped = true;
            _manager.IsMoving = false;
            transform.position = groundDetector.CurrentGorund.transform.position;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z); 
        }

        public void OnRestartLevel()
        {
            _isNotStarted = true;
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
            [SerializeField] public AnimationCurve SpeedCurve;
        }
    }
}