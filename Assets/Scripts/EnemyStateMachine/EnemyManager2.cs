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

public class EnemyManager2 : MonoBehaviour
{
    #region Self Variables

    #region Inject Variables
    [Inject] private CoreGameSignals CoreGameSignals { get; set; }
    [Inject] private EnemyInternalSignals EnemyInternalSignals { get; set; }
    [Inject] private EnemyAnimationController animationController;
    [Inject] private StateMachine StateMachine;

    #endregion

    #region Serialized Variables

    [SerializeField] private EnemyStateEnums currentStateEnum;


    #endregion
    #region Private Variables
    private bool _isDead, 
        _isHitted = false;

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
        _isHitted = false;
        _isDead = false;
        currentStateEnum = EnemyStateEnums.Move;
        StateMachine.InitMachine(EnemyStateEnums.Move);
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.onRestart += OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation += animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath += OnDied;
        EnemyInternalSignals.onHitted += OnHitted;
        EnemyInternalSignals.onChangeState += OnChangeState;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.onRestart -= OnRestartLevel;

        EnemyInternalSignals.onChangeAnimation -= animationController.OnChangeAnimation;
        EnemyInternalSignals.onDeath -= OnDied;
        EnemyInternalSignals.onHitted -= OnHitted;
        EnemyInternalSignals.onChangeState -= OnChangeState;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        StateMachine.OnResetCurrentState();
    }

    #endregion

    private void Update()
    {
        if (_isDead)
        {
            return;
        }
        StateMachine.Tick();
    }

    private void OnDied(IAttackable attackable)
    {
        _isDead = true;
    }

    public void OnHitted()
    {
        OnChangeState(EnemyStateEnums.Any);
        _isHitted = true;
        TakeDamageDelay(2f);
    }

    private async Task TakeDamageDelay(float value)
    {
        await Task.Delay(System.TimeSpan.FromSeconds(value));
        _isHitted = false;
        OnChangeState(EnemyStateEnums.Move);
    }

    public void OnChangeState(EnemyStateEnums newState)
    {
        if (_isDead || _isHitted)
        {
            return;
        }

        StateMachine.ChangeState(newState);
        currentStateEnum = newState;
    }

    private void OnRestartLevel()
    {
        gameObject.SetActive(false);
    }
}
