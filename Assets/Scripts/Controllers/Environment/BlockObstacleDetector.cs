using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlockObstacleDetector : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables

    #endregion

    #region Public Variables

    #endregion 

    #region Serialized Variables
    [SerializeField] private BoxCollider myCollider;
    #endregion

    #region Private Variables

    #endregion

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            myCollider.enabled = false;
        }
    }
}
