using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerGroundDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private PlayerSignals PlayerSignals { get; set; }

    #endregion

    #region Public Variables
    public GameObject CurrentGround = null;
    public GameObject PreviusGround = null;
    #endregion 

    #region Serialized Variables

    #endregion

    #region Private Variables

    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            PlayerSignals.onPlayerStopped?.Invoke();
            PlayerSignals.onChangeAnimation?.Invoke(Enums.PlayerAnimationStates.Attack);
        }

        if (other.CompareTag("Ground"))
        {
            PreviusGround = CurrentGround;
            CurrentGround = other.gameObject;
        }
    }
}
