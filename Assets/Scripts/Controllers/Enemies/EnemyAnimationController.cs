using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController
{
    #region Self Variables

    #region Serialized Variables

    #endregion
    #region Private Variables

    #endregion
    #endregion

    private Animator _animator;

    public EnemyAnimationController(Animator animator)
    {
        _animator = animator;
    }
    public void ChangeAnimation(EnemyAnimationStates nextAnimation)
    {
        _animator.SetTrigger(nextAnimation.ToString());
    }

    public void ResetTrigger(EnemyAnimationStates resetAnimation)
    {
        _animator.ResetTrigger(resetAnimation.ToString());
    }
}