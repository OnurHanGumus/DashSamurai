using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;

namespace Installers.Prefabs
{
    public class BomberInstaller : EnemyInstaller
    {
        private BomberUiSettings _bomberUiSettings;

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
            _enemySettings = Resources.Load<EnemySettings>("Data/Enemies/BomberSettings");
            Container.BindInstance(_enemySettings).AsSingle();

            _bomberUiSettings = Resources.Load<BomberUiSettings>("Data/Enemies/BomberUiSettings");
            Container.BindInstance(_bomberUiSettings).AsSingle();
        }

        protected override void BindReferences()
        {
            base.BindReferences();
        }

        protected override void BindConditions()
        {
            base.BindConditions();
            attackCondition = new BomberAttackCondition(playerTransform, myTransform);
            Container.QueueForInject(attackCondition);
        }

        protected override void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, anyCondition);
            conditionInAttackState = new Conditions(moveCondition, anyCondition);
            conditionInAnyState = new Conditions(moveCondition, attackCondition, anyCondition);
        }

        protected override void BindStates()
        {
            base.BindStates();
            attackState = new BomberAttackState(playerTransform, myTransform, conditionInAttackState, physicsController);
            Container.QueueForInject(attackState);
        }

        protected override void BindStateMachine()
        {
            base.BindStateMachine();
        }
    }
}