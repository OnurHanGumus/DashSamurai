using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Controllers;

namespace Installers.Prefabs
{
    public class MinatourInstaller : EnemyInstaller
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
            _enemySettings = Resources.Load<EnemySettings>("Data/Bosses/MinatourSettings");
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

        protected virtual void BindTransitions()
        {
            conditionInMoveState = new Conditions(attackCondition, deadCondition);
            conditionInAttackState = new Conditions(moveCondition, deadCondition);
            conditionInAnyState = new Conditions(moveCondition, deadCondition, attackCondition);
        }

        protected override void BindStates()
        {
            base.BindStates();
        }

        protected override void BindStateMachine()
        {
            base.BindStateMachine();
        }
    }
}