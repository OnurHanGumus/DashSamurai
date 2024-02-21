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
            _enemySettings = Resources.Load<EnemySettings>("Data/Enemies/ZombieSettings");
            Container.BindInstance(_enemySettings).AsSingle();
        }

        protected virtual void BindReferences()
        {
            Container.BindInstance(navMeshAgent).AsSingle();
            _enemyAnimationController = new EnemyAnimationController(animator);
            Container.BindInstance(_enemyAnimationController).AsSingle();
            Container.BindInstance(manager).AsSingle();
            Container.BindInstance(physicsController).AsSingle();
        }

        protected virtual void BindConditions()
        {
            moveCondition = new MoveCondition(playerTransform, myTransform);
            Container.QueueForInject(moveCondition);

            attackCondition = new AttackCondition(playerTransform, myTransform);
            Container.QueueForInject(attackCondition);

            anyCondition = new AnyCondition();
            Container.QueueForInject(anyCondition);

            deadCondition = new DeadCondition();
            Container.QueueForInject(deadCondition);
        }

        protected virtual void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, anyCondition, deadCondition);
            conditionInAttackState = new Conditions(moveCondition, anyCondition, deadCondition);
            conditionInAnyState = new Conditions(moveCondition, deadCondition, attackCondition, anyCondition);
        }

        protected virtual void BindStates()
        {
            moveState = new MoveState(playerTransform, conditionInMoveState);
            Container.QueueForInject(moveState);

            anyState = new AnyState(conditionInAnyState);
            Container.QueueForInject(anyState);

            attackState = new AttackState(playerTransform, myTransform, conditionInAttackState);
            Container.QueueForInject(attackState);

            deadState = new DeadState();
            Container.QueueForInject(deadState);

        }

        protected virtual void BindStateMachine()
        {
            stateMachine = new StateMachine(manager, _stateMachineInternalSignals, anyState, moveState, attackState, deadState);
            Container.BindInstance(stateMachine).AsSingle();
        }
    }
}