using Events.External;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BulletMovementController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region Serializefield Variables
    [SerializeField] private int speed = 5;
    #endregion
    #region Private Variables
    private Rigidbody _rig;
    #endregion
    #endregion
    private void OnEnable()
    {
        MoveForward();
    }
    private void OnDisable()
    {
        _rig.velocity = Vector3.zero;

    }
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rig = GetComponent<Rigidbody>();
    }


    private void MoveForward()
    {
        _rig.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

    }

}
