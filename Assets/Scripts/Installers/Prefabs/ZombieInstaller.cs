﻿using Data.MetaData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;

namespace Installers.Prefabs
{
    public class ZombieInstaller : MonoInstaller<ZombieInstaller>
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform myTransform;

        [SerializeField] private EnemyManager2 manager;
        [SerializeField] private EnemyPhysicsController physicsController;
        [SerializeField] private Animator animator;

        [Inject(Id = "Player")] private Transform playerTransform;

        EnemyInternalSignals _enemyInternalSignals;
        StateMachineInternalSignals _stateMachineInternalSignals;
        private EnemySettings _enemySettings;
        private EnemyAnimationController _enemyAnimationController;


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

            StateMachine stateMachine = new StateMachine(manager, _stateMachineInternalSignals, anyState, moveState, attackState, deadState);
            Container.BindInstance(stateMachine).AsSingle();
        }

        private void BindSignals()
        {
            _enemyInternalSignals = new EnemyInternalSignals();
            _stateMachineInternalSignals = new StateMachineInternalSignals();

            Container.BindInstance(_stateMachineInternalSignals).AsSingle();
            Container.BindInstance(_enemyInternalSignals).AsSingle();
        }

        private void BindData()
        {
            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/ZombieSettings");
            Container.BindInstance(_enemySettings).AsSingle();
        }

        private void BindReferences()
        {
            Container.BindInstance(navMeshAgent).AsSingle();
            _enemyAnimationController = new EnemyAnimationController(animator); 
            Container.BindInstance(_enemyAnimationController).AsSingle();
        }

        private void BindConditions()
        {
            moveCondition = new MoveCondition(manager, physicsController, playerTransform, myTransform, _stateMachineInternalSignals, _enemySettings);

            attackCondition = new AttackCondition(manager, physicsController, playerTransform, myTransform, _stateMachineInternalSignals, _enemySettings);

            anyCondition = new AnyCondition(physicsController, _stateMachineInternalSignals);

            deadCondition = new DeadCondition(physicsController, _stateMachineInternalSignals);
        }

        private void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, anyCondition, deadCondition);
            conditionInAttackState = new Conditions(moveCondition, anyCondition, deadCondition);
            conditionInAnyState = new Conditions(moveCondition, deadCondition, attackCondition, anyCondition);
        }

        private void BindStates()
        {
            moveState = new MoveState(navMeshAgent, manager, playerTransform, conditionInMoveState, _enemyAnimationController, _enemySettings);
            anyState = new AnyState(navMeshAgent, _enemyAnimationController, conditionInAnyState, _enemySettings);
            attackState = new AttackState(navMeshAgent, playerTransform, myTransform, conditionInAttackState, _enemySettings, _enemyAnimationController);
            deadState = new DeadState(navMeshAgent);
            Container.QueueForInject(deadState);

        }
    }
}