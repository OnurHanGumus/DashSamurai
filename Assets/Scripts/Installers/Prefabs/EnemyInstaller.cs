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

        private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            EnemyInternalSignals enemyInternalSignals = new EnemyInternalSignals();
            StateMachineInternalSignals stateMachineInternalSignals = new StateMachineInternalSignals();
            Container.BindInstance(stateMachineInternalSignals).AsSingle();
            Container.BindInstance(enemyInternalSignals).AsSingle();

            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/EnemySettings");
            Container.BindInstance(_enemySettings).AsSingle();

            Container.BindInstance(animationController).AsSingle();
            Container.BindInstance(navMeshAgent).AsSingle();

            MoveCondition moveCondition = new MoveCondition(manager, physicsController, playerTransform, myTransform, stateMachineInternalSignals);
            Container.BindInstance(moveCondition).AsSingle();

            AttackCondition attackCondition = new AttackCondition(manager, physicsController, playerTransform, myTransform, stateMachineInternalSignals);
            Container.BindInstance(attackCondition).AsSingle();

            AnyCondition anyCondition = new AnyCondition(physicsController, stateMachineInternalSignals);
            Container.BindInstance(anyCondition).AsSingle();

            DeadCondition deadCondition = new DeadCondition(physicsController, stateMachineInternalSignals);
            Container.BindInstance(deadCondition).AsSingle();

            Conditions conditionInMoveState = new Conditions(attackCondition, anyCondition, deadCondition);
            Container.BindInstance(conditionInMoveState).AsTransient();

            Conditions conditionInAttackState = new Conditions(moveCondition, anyCondition, deadCondition);
            Container.BindInstance(conditionInAttackState).AsTransient();

            Conditions conditionInAnyState = new Conditions(moveCondition, deadCondition);
            Container.BindInstance(conditionInAnyState).AsTransient();

            MoveState moveState = new MoveState(navMeshAgent, playerTransform, myTransform, conditionInMoveState, enemyInternalSignals);
            AnyState anyState = new AnyState(navMeshAgent, conditionInAnyState);
            AttackState attackState = new AttackState(navMeshAgent, playerTransform, myTransform, enemyInternalSignals, conditionInAttackState);
            DeadState deadState = new DeadState(navMeshAgent);

            Container.BindInstance(moveState).AsSingle();
            Container.BindInstance(anyState).AsSingle();
            Container.BindInstance(attackState).AsSingle();
            Container.BindInstance(deadState).AsSingle();

            StateMachine stateMachine = new StateMachine(manager, stateMachineInternalSignals, anyState, moveState, attackState, deadState);
            Container.BindInstance(stateMachine).AsSingle();
        }
    }
}