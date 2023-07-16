using Data.MetaData;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Installers.Prefabs
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemyAnimationController animationController;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private Transform myTransform;

        [Inject(Id = "Player")] private Transform playerTransform;

        private EnemySettings _enemySettings;

        public override void InstallBindings()
        {
            EnemyInternalSignals enemyInternalSignals = new EnemyInternalSignals();
            Container.BindInstance(enemyInternalSignals).AsSingle();

            _enemySettings = Resources.Load<EnemySettings>("Data/MetaData/EnemySettings");
            Container.BindInstance(_enemySettings).AsSingle();

            Container.BindInstance(animationController).AsSingle();
            Container.BindInstance(navMeshAgent).AsSingle();

            MoveState moveState = new MoveState(navMeshAgent, playerTransform, myTransform);
            AnyState anyState = new AnyState(navMeshAgent);
            AttackState attackState = new AttackState(navMeshAgent, playerTransform, myTransform, enemyInternalSignals);

            Container.BindInstance(moveState).AsSingle();
            Container.BindInstance(anyState).AsSingle();
            Container.BindInstance(attackState).AsSingle();

            StateMachine stateMachine = new StateMachine(anyState, moveState, attackState);
            Container.BindInstance(stateMachine).AsSingle();
        }
    }
}