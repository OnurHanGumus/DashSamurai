using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;


public class HealthBarManager : SpriteRotater
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
    protected override void Awake()
    {
        Init();
    }

    protected override void Init()
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    protected void SetHealthBarScale(int currentValue)//HealthBar increase or decrease with this method. This method can also listen a signal.
    {
        healthBar.localScale = new Vector3((float)currentValue / 100, 1, 1);
    }
}
