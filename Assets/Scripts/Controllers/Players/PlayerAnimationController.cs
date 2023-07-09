using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Data.ValueObject;
using Data.UnityObject;

public class PlayerAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Animator animator;

    #endregion
    #region Private Variables
    private UIData _uiData;

    #endregion
    #endregion

    public UIData GetData() => Resources.Load<CD_UI>("Data/CD_UI").Data;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _uiData = GetData();
    }

    public void OnChangeAnimation(PlayerAnimationStates nextAnimation)
    {
        animator.SetTrigger(nextAnimation.ToString());
    }

    public void OnResetTrigger(PlayerAnimationStates resetAnimation)
    {
        animator.ResetTrigger(resetAnimation.ToString());
    }

    public void OnRestartLevel()
    {
        animator.Rebind();
        animator.Update(0f);
    }
}