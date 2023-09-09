using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables

    #endregion

    #region Public Variables
    public GameObject CurrentGround = null;
    #endregion 

    #region Serialized Variables

    #endregion

    #region Private Variables

    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            CurrentGround = other.gameObject;
        }
    }
}
