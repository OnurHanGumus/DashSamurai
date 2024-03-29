﻿using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;
using Signals;

namespace Installers.Prefabs
{
    public class WizardInstaller : EnemyInstaller
    {
        [SerializeField] private Transform mageInitTransform;

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

        protected override void BindSignals()
        {
            base.BindSignals();
        }

        protected override void BindData()
        {
            _enemySettings = Resources.Load<EnemySettings>("Data/Enemies/WizardSettings");
            Container.BindInstance(_enemySettings).AsSingle();
        }

        protected override void BindReferences()
        {
            base.BindReferences();
        }

        protected override void BindConditions()
        {
            base.BindConditions();
        }

        protected override void BindTransitions()
        {
            base.BindTransitions();
        }

        protected override void BindStates()
        {
            base.BindStates();
            attackState = new WizardAttackState(playerTransform, myTransform, conditionInAttackState, mageInitTransform);
            Container.QueueForInject(attackState);
            Container.BindInterfacesTo(attackState.GetType()).FromInstance(attackState);

        }

        protected override void BindStateMachine()
        {
            base.BindStateMachine();
        }
    }
}