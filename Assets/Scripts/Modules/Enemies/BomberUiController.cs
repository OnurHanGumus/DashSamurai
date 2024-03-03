using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using Zenject;

public class BomberUiController : MonoBehaviour
{
    #region Self Variables
    #region Injected Variables
    [Inject] private BomberUiSettings _uiSettings { get; set; }
    #endregion

    #region Public Variables


    #endregion
    #region Serialized Variables
    [SerializeField] private TextMeshPro text;
    [SerializeField] private BomberTimer bomberTimer;
    #endregion
    #region Private Variables

    #endregion
    #endregion

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        bomberTimer.onTimeUpdated += Ticked;
    }

    private void Ticked(int value)
    {
        if (value > _uiSettings.TextAppearedAtSecond)
        {
            return;
        }
        text.transform.localScale = Vector3.zero;
        text.text = "0" + value.ToString();
        text.transform.DOScale(1f, _uiSettings.TextScaleAnimationDelay);
    }

    private void OnEnable()
    {
        text.text = "";
    }
}
