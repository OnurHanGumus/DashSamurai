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
        [Inject] private AudioSignals _audioSignals { get; set; }

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

            _rig.velocity = (isPlayerStopped ? 0 : 1) * _input * PlayerSettings.Speed * PlayerSettings.SpeedCurve.Evaluate(_currentTime);
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
            PlayerSignals.onUseAbility?.Invoke(-30);
            _audioSignals.onPlaySound?.Invoke(Enums.AudioSoundEnums.DashIn);
        }

        public void OnPlayerStopped()
        {
            _rig.velocity = Vector3.zero;
            isPlayerStopped = true;
            _manager.IsMoving = false;
            Vector3 newPos = groundDetector.CurrentGround.transform.position;
            transform.position = new Vector3(newPos.x, 0.5f, newPos.z);
            _audioSignals.onPlaySound?.Invoke(Enums.AudioSoundEnums.DashOut);
        }

        public void OnRestartLevel()
        {
            _isNotStarted = true;
            _pastInput = new Vector3();
        }

        [Serializable]
        public class Settings
        {
            [SerializeField] public float Speed = 1f;
            [SerializeField] public AnimationCurve SpeedCurve;
            [SerializeField] public Quaternion InitialQuaternion;
        }
    }
}