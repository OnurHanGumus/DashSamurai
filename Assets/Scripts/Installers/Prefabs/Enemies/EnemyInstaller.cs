using Data.MetaData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;
using System;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] protected NavMeshAgent navMeshAgent;
        [SerializeField] protected Transform myTransform;


        [SerializeField] protected EnemyManager2 manager;
        [SerializeField] protected EnemyPhysicsController physicsController;
        [SerializeField] protected Animator animator;

        [Inject(Id = "Player")] protected Transform playerTransform;

        protected EnemyInternalSignals _enemyInternalSignals;
        protected StateMachineInternalSignals _stateMachineInternalSignals;
        protected EnemySettings _enemySettings;
        protected EnemyAnimationController _enemyAnimationController;


        protected ICondition moveCondition;
        protected ICondition attackCondition;
        protected ICondition anyCondition;
        protected ICondition deadCondition;

        protected Conditions conditionInMoveState;
        protected Conditions conditionInAttackState;
        protected Conditions conditionInAnyState;

        protected IState moveState;
        protected IState anyState;
        protected IState attackState;
        protected IState deadState;

        protected StateMachine stateMachine;

        public override void InstallBindings()
        {

            BindSignals();
            BindData();
            BindReferences();
            BindConditions();
            BindTransitions();
            BindStates();
            BindStateMachine();

        }

        protected virtual void BindSignals()
        {
            _enemyInternalSignals = new EnemyInternalSignals();
            _stateMachineInternalSignals = new StateMachineInternalSignals();

            Container.BindInstance(_stateMachineInternalSignals).AsSingle();
            Container.BindInstance(_enemyInternalSignals).AsSingle();
        }

        protected virtual void BindData()
        {
            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/ZombieSettings");
            Container.BindInstance(_enemySettings).AsSingle();
        }

        protected virtual void BindReferences()
        {
            Container.BindInstance(navMeshAgent).AsSingle();
            _enemyAnimationController = new EnemyAnimationController(animator);
            Container.BindInstance(_enemyAnimationController).AsSingle();
        }

        protected virtual void BindConditions()
        {
            moveCondition = new MoveCondition(manager, physicsController, playerTransform, myTransform, _stateMachineInternalSignals, _enemySettings);

            attackCondition = new AttackCondition(manager, physicsController, playerTransform, myTransform, _stateMachineInternalSignals, _enemySettings);

            anyCondition = new AnyCondition(physicsController, _stateMachineInternalSignals);

            deadCondition = new DeadCondition(physicsController, _stateMachineInternalSignals);
        }

        protected virtual void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, anyCondition, deadCondition);
            conditionInAttackState = new Conditions(moveCondition, anyCondition, deadCondition);
            conditionInAnyState = new Conditions(moveCondition, deadCondition, attackCondition, anyCondition);
        }

        protected virtual void BindStates()
        {
            moveState = new MoveState(navMeshAgent, manager, playerTransform, conditionInMoveState, _enemyAnimationController, _enemySettings);
            anyState = new AnyState(navMeshAgent, _enemyAnimationController, conditionInAnyState, _enemySettings);
            attackState = new AttackState(navMeshAgent, playerTransform, myTransform, conditionInAttackState, _enemySettings, _enemyAnimationController);
            deadState = new DeadState(navMeshAgent);
            Container.QueueForInject(deadState);

        }

        protected virtual void BindStateMachine()
        {
            stateMachine = new StateMachine(manager, _stateMachineInternalSignals, anyState, moveState, attackState, deadState);
            Container.BindInstance(stateMachine).AsSingle();
        }
    }
}