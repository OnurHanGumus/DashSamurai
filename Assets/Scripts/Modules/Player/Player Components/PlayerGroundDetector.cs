using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerGroundDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }
    [Inject] private CoreGameSignals _coreGameSignals { get; set; }

    #endregion

    #region Public Variables
    public GameObject CurrentGround = null;
    #endregion

    #region Serialized Variables
    [SerializeField] private Collider collider;
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
        _coreGameSignals.onPlay += EnableCollider;
    }

    private void UnsubscribeEvents()
    {
        _coreGameSignals.onPlay -= EnableCollider;
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
            Debug.Log("stopped");
            PlayerSignals.onPlayerStopped?.Invoke();
            PlayerSignals.onChangeAnimation?.Invoke(Enums.PlayerAnimationStates.Attack);
        }

        if (other.CompareTag("Ground"))
        {
            CurrentGround = other.gameObject;
        }
    }

    private void EnableCollider()
    {
        collider.enabled = true;
    }

    private void DisableCollider()
    {
        collider.enabled = false;
    }
}
