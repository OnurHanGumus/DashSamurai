using Cinemachine;
using Signals;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private CameraSignals CameraSignals { get; set; }

        #endregion
        #region Public Variables

        public CameraStatesEnum CameraStateController
        {
            get => _cameraStateValue;
            set
            {
                _cameraStateValue = value;
                SetCameraStates();
            }
        }

        #endregion
        #region Serialized Variables
        [SerializeField] private CameraStatesEnum _cameraStateValue;
        #endregion

        #region Private Variables

        private Vector3 _initialPosition;
        private Animator _camAnimator;
        [SerializeField] private CinemachineVirtualCameraBase VCBase;


        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            _camAnimator = GetComponent<Animator>();
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CameraSignals.onChangeState += OnChangeState;
            CameraSignals.onSetTarget += OnSetTarget;
        }

        private void UnsubscribeEvents()
        {
            CameraSignals.onChangeState -= OnChangeState;
            CameraSignals.onSetTarget -= OnSetTarget;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void SetCameraStates()
        {
            _camAnimator.Play(CameraStateController.ToString());
        }

        private void OnChangeState(CameraStatesEnum newState)
        {
            CameraStateController = newState;
        }

        private void OnSetTarget(Transform target)
        {
            VCBase.Follow = target;
        }
    }
}