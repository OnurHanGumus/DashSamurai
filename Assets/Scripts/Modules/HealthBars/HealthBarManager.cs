using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;


public class HealthBarManager : MonoBehaviour
{
    #region Self Variables
    #region Inject Variables
    #endregion
    #region Public Variables

    #endregion

    #region Serialized Variables
    [SerializeField] private Transform healthBar;
    #endregion

    #region Private Variables

    #endregion

    #endregion
    protected void Awake()
    {
        Init();
    }

    protected void Init()
    {

    }

    protected virtual void Update()
    {
        transform.eulerAngles = new Vector3(60, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    protected void SetHealthBarScale(int currentValue)//HealthBar increase or decrease with this method. This method can also listen a signal.
    {
        healthBar.localScale = new Vector3((float)currentValue / 100, 1, 1);
    }
}
