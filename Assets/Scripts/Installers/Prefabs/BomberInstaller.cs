using Data.MetaData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;

namespace Installers.Prefabs
{
    public class BomberInstaller : EnemyInstaller
    {
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
            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/BomberSettings");
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
            attackState = new BomberAttackState(navMeshAgent, playerTransform, myTransform, conditionInAttackState, _enemySettings, _enemyAnimationController);
            Container.QueueForInject(attackState);
        }

        protected override void BindStateMachine()
        {
            base.BindStateMachine();
        }
    }
}