using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerGroundDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PlayerSignals _playerSignals { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }

    #endregion

    #region Public Variables
    public GameObject CurrentGround = null;
    #endregion

    #region Serialized Variables
    [SerializeField] private Collider detector;
    #endregion

    #region Private Variables

    #endregion

    #endregion

    #region Event Subscription

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _coreGameSignals.onPlay += EnableDetector;
    }

    private void UnsubscribeEvents()
    {
        _coreGameSignals.onPlay -= EnableDetector;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            _playerSignals.onPlayerStopped?.Invoke();
            _playerSignals.onChangeAnimation?.Invoke(Enums.PlayerAnimationStates.Attack);
        }

        if (other.CompareTag("Ground"))
        {
            CurrentGround = other.gameObject;
        }
    }

    private void EnableDetector()
    {
        detector.enabled = true;
    }
}
