using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Data.ValueObject;

public class PlayerAnimationController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private Animator animator;

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