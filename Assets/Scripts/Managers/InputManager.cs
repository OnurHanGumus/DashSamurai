using System;
using System.Collections.Generic;
using Data.UnityObject;
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
        #endregion
        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables

        //[SerializeField] FloatingJoystick joystick; //SimpleJoystick paketi eklenmeli


        #endregion

        #region Private Variables


        private float _currentVelocity; //ref type
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private bool _isPlayerDead = false;
        private Ray _ray;
        private Transform _clickedTransform;

        private bool _isBoomerangDisapeared = false;
        private bool _isPlayerDrawing = false;
        private bool _isBoomerangOnPlayer = true;
        #endregion

        #endregion


        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").Data;


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
        }

        private void UnsubscribeEvents()
        {
            InputSignals.onEnableInput -= OnEnableInput;
            InputSignals.onDisableInput -= OnDisableInput;
            CoreGameSignals.onPlay -= OnPlay;
            CoreGameSignals.onReset -= OnReset;
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
            //if (Input.GetMouseButton(0))
            //{
            //    if (!_clickedTransform.gameObject.CompareTag("Car"))
            //    {
            //        return;
            //    }
            //    InputSignals.onInputDragged?.Invoke(new Vector2()
            //    {
            //        x = joystick.Horizontal,
            //        y = joystick.Vertical,
            //    });
            //}

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
            return results.Count > 0;
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

        }

        private void OnBoomerangDisapeared()
        {
            _isBoomerangDisapeared = true;
        }

        private void OnBoomerangRebuilded()
        {
            _isBoomerangDisapeared = false;
        }

        private void OnBoomerangReturned()
        {
            _clickedTransform = null;
            _isBoomerangOnPlayer = true;
        }
        private void OnBoomerangThrowed()
        {
            _isBoomerangOnPlayer = false;
        }

        //private bool IsPointerOverUIElement() //Joystick'i do�ru konumland�r�rsan buna gerek kalmaz
        //{
        //    var eventData = new PointerEventData(EventSystem.current);
        //    eventData.position = Input.mousePosition;
        //    var results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(eventData, results);
        //    return results.Count > 0;
        //}

        private void OnReset()
        {
            _isBoomerangOnPlayer = true;
            _clickedTransform = null;
        }

        private void OnChangePlayerLivingState()
        {
            _isPlayerDead = !_isPlayerDead;
        }

    }
}