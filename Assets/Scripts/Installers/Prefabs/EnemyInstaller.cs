using Data.MetaData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform myTransform;

        [SerializeField] private EnemyManager2 manager;
        [SerializeField] private EnemyPhysicsController physicsController;

        [Inject(Id = "Player")] private Transform playerTransform;

        EnemyInternalSignals enemyInternalSignals;
        StateMachineInternalSignals stateMachineInternalSignals;
        private EnemySettings _enemySettings;


        MoveCondition moveCondition;
        AttackCondition attackCondition;
        AnyCondition anyCondition;
        DeadCondition deadCondition;

        Conditions conditionInMoveState;
        Conditions conditionInAttackState;
        Conditions conditionInAnyState;

        MoveState moveState;
        AnyState anyState;
        AttackState attackState;
        DeadState deadState;

        public override void InstallBindings()
        {

            BindSignals();
            BindData();
            BindReferences();
            BindConditions();
            BindTransitions();
            BindStates();

            StateMachine stateMachine = new StateMachine(manager, stateMachineInternalSignals, anyState, moveState, attackState, deadState);
            Container.BindInstance(stateMachine).AsSingle();
        }

        private void BindSignals()
        {
            enemyInternalSignals = new EnemyInternalSignals();
            stateMachineInternalSignals = new StateMachineInternalSignals();

            Container.BindInstance(stateMachineInternalSignals).AsSingle();
            Container.BindInstance(enemyInternalSignals).AsSingle();
        }

        private void BindData()
        {
            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/EnemySettings");
            Container.BindInstance(_enemySettings).AsSingle();
        }

        private void BindReferences()
        {
            Container.BindInstance(animationController).AsSingle();
            Container.BindInstance(navMeshAgent).AsSingle();
        }

        private void BindConditions()
        {
            moveCondition = new MoveCondition(manager, physicsController, playerTransform, myTransform, stateMachineInternalSignals, _enemySettings);
            Container.BindInstance(moveCondition).AsSingle();

            attackCondition = new AttackCondition(manager, physicsController, playerTransform, myTransform, stateMachineInternalSignals, _enemySettings);
            Container.BindInstance(attackCondition).AsSingle();

            anyCondition = new AnyCondition(physicsController, stateMachineInternalSignals);
            Container.BindInstance(anyCondition).AsSingle();

            deadCondition = new DeadCondition(physicsController, stateMachineInternalSignals);
            Container.BindInstance(deadCondition).AsSingle();
        }

        private void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, anyCondition, deadCondition);
            Container.BindInstance(conditionInMoveState).AsTransient();

            conditionInAttackState = new Conditions(moveCondition, anyCondition, deadCondition);
            Container.BindInstance(conditionInAttackState).AsTransient();

            conditionInAnyState = new Conditions(moveCondition, deadCondition, attackCondition);
            Container.BindInstance(conditionInAnyState).AsTransient();
        }

        private void BindStates()
        {
            moveState = new MoveState(manager, navMeshAgent, playerTransform, conditionInMoveState, enemyInternalSignals, _enemySettings);
            anyState = new AnyState(navMeshAgent, conditionInAnyState, _enemySettings);
            attackState = new AttackState(navMeshAgent, playerTransform, myTransform, enemyInternalSignals, conditionInAttackState, _enemySettings);
            deadState = new DeadState(navMeshAgent);

            Container.BindInstance(moveState).AsSingle();
            Container.BindInstance(anyState).AsSingle();
            Container.BindInstance(attackState).AsSingle();
            Container.BindInstance(deadState).AsSingle();
        }
    }
}