using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Enums;
using Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComboPanelController : MonoBehaviour
{
    #region Self Variables
    #region Public Variables
    #endregion
    #region SerializeField Variables
    [SerializeField] private TextMeshProUGUI commentTxt;
    [SerializeField] private Transform comboPanel;
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
        gameObject.transform.localScale = Vector3.zero;
    }

    public void OnCombo(int value)
    {
        //StartCoroutine(Effect());
        //commentTxt.text = _commentsData.CommentsList[value];
        //ScoreSignals.onScoreIncrease?.Invoke(ScoreTypeEnums.Gem, _gainMoneyData.GainMoneyList[value]);
        //AudioSignals.onPlaySound(AudioSoundEnums.Combo);
    }


    private IEnumerator Effect()
    {
        comboPanel.DOScale(1, 0.5f).SetEase(Ease.Flash);
        yield return new WaitForSeconds(1.5f);
        comboPanel.localScale = Vector3.zero;
    }

    public void OnRestartLevel()
    {
    }
}
