using System;
using System.Collections.Generic;
using Data.ValueObject;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Enums;
using Zenject;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables
        #region Inject Variables
        [Inject] private InputSignals InputSignals { get; set; }
        [Inject] private CoreGameSignals CoreGameSignals { get; set; }
        [Inject] private PlayerSignals PlayerSignals { get; set; }
        [Inject] private UISignals _uiSignals { get; set; }
        #endregion
        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] FloatingJoystick joystick; //SimpleJoystick paketi eklenmeli

        #endregion

        #region Private Variables


        private float _currentVelocity; //ref type
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private bool _isPlayerDead = false;
        private bool _isReadyToInput = false;
        private Ray _ray;
        private Transform _clickedTransform;
        #endregion

        #endregion

        private void Awake()
        {

        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.onEnableInput += OnEnableInput;
            InputSignals.onDisableInput += OnDisableInput;
            CoreGameSignals.onPlay += OnPlay;
            CoreGameSignals.onReset += OnReset;
            _uiSignals.onLockJoystick += OnLockJoystick;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.onEnableInput -= OnEnableInput;
            InputSignals.onDisableInput -= OnDisableInput;
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onReset -= OnReset;
            _uiSignals.onLockJoystick -= OnLockJoystick;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(_ray, out hit))
                {
                    _clickedTransform = hit.transform;
                }
                InputSignals.onClicked?.Invoke(/*_clickedTransform*/);
            }

            if (Input.GetMouseButton(0))
            {
                if (IsPointerOverUIElement())
                {
                    return;
                }

                if (PlayerSignals.onGetStaminaValue() < 50)
                {
                    PlayerSignals.onLowStamina?.Invoke();
                    return;
                }

                InputSignals.onInputDragged?.Invoke(new Vector3()
                {
                    x = joystick.Horizontal,
                    z = joystick.Vertical
                });
            }

            if (Input.GetMouseButtonUp(0))
            {
                InputSignals.onInputReleased?.Invoke();
            }
        }

        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 1;
            //return EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0);
        }

        private void OnEnableInput()
        {

        }

        private void OnDisableInput()
        {

        }

        private void OnPlay()
        {
            _isReadyToInput = true;
        }

        private void OnLockJoystick(bool isLock)
        {
            joystick.enabled = !isLock;
        }

        private void OnReset()
        {
            _clickedTransform = null;
        }

        private void OnChangePlayerLivingState()
        {
            _isPlayerDead = !_isPlayerDead;
        }
    }
}