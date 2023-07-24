using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Data.ValueObject;
using Data.UnityObject;

public class EnemyAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject rootPartToReset;
    [SerializeField] private Vector3 rootPartToValues;

    #endregion
    #region Private Variables
    private UIData _uiData;

    #endregion
    #endregion


    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    private void OnDisable()
    {
        rootPartToReset.transform.localPosition = rootPartToValues;
    }

    public void OnChangeAnimation(EnemyAnimationStates nextAnimation)
    {
        animator.SetTrigger(nextAnimation.ToString());
    }

    public void OnResetTrigger(EnemyAnimationStates resetAnimation)
    {
        animator.ResetTrigger(resetAnimation.ToString());
    }

    public void OnRestartAnimator()
    {
        animator.Rebind();
        animator.Update(0f);
    }
}