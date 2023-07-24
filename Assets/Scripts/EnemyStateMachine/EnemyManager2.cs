using Components.Players;
using Enums;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AI;
using Components.Enemies;
using System.Threading.Tasks;
using System.Threading;

public class EnemyManager2 : MonoBehaviour
{
    #region Self Variables

    #region Inject Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
    [Inject] private EnemyAnimationController animationController;
    [Inject] private StateMachine StateMachine;

    #endregion
    #region Public Variables
    public EnemyStateEnums CurrentStateEnum;

    #endregion
    #region Serialized Variables



    #endregion
    #region Private Variables
    private bool _isDead;

    #endregion
    #endregion

    [Inject]
    public void Construct(CoreGameSignals coreGameSignals)
    {
        CoreGameSignals = coreGameSignals;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    #region Event Subscriptions
    private void OnEnable()
    {
        SubscribeEvents();
        _isDead = false;
        CurrentStateEnum = EnemyStateEnums.Move;
        StateMachine.InitMachine(EnemyStateEnums.Move);
        EnemyInternalSignals.onResetAnimation?.Invoke(EnemyAnimationStates.Attack1);
        EnemyInternalSignals.onResetAnimation?.Invoke(Enums.EnemyAnimationStates.Move);
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemyInternalSignals.onResetAnimation += animationController.OnResetTrigger;
        EnemyInternalSignals.onChangeAnimation += animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath += OnDied;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemyInternalSignals.onResetAnimation -= animationController.OnResetTrigger;
        EnemyInternalSignals.onChangeAnimation -= animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath -= OnDied;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        StateMachine.OnResetCurrentState();
    }

    #endregion

    private void Update()
    {
        StateMachine.Tick();
    }

    private void OnDied(IAttackable attackable)
    {
        _isDead = true;
    }

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
}