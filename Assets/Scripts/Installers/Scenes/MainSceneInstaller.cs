using Zenject;
using UnityEngine;
using Data.MetaData;
using Signals;

namespace Installers.Scenes
{
    public class MainSceneInstaller : MonoInstaller<MainSceneInstaller>
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform playerTransform;

        [SerializeField] private GameObject shieldParticle, staminaParticle, 
            poisonParticle, dashParticle;

        [SerializeField] private SphereCollider poisonParticleCollider;

        private BulletSettings _bulletSettings;
        private AbilitySettings _abilitySettings;
        private CD_EnemySpawn _enemySpawnSettings;
        


        public override void InstallBindings()
        {
            BindComponents();
            BindSettings();
        }

        void BindComponents()
        {
            Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<LevelSignals>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<ScoreSignals>().AsSingle();
            Container.Bind<SaveSignals>().AsSingle();
            Container.Bind<PoolSignals>().AsSingle();
            Container.Bind<AudioSignals>().AsSingle();
            Container.Bind<PlayerSignals>().AsSingle();
            Container.Bind<EnemySignals>().AsSingle();
            Container.Bind<CameraSignals>().AsSingle();

            //Container.Bind<DenemeController>().AsSingle();
            //Container.BindInterfacesTo<DenemeController>().with.FromResolve();
            //Container.Bind<IDeneme>().WithId("DenemeController").To<DenemeController>().AsSingle();
            //Container.Bind<IDeneme>().WithId("DenemeController2").To<DenemeController2>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemyTypeSelector>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnPointSelector>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaveTimer>().AsSingle();
            Container.BindInterfacesAndSelfTo<WaveManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawnManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectableSpawnManager>().AsSingle();
            Container.BindInstance(playerTransform).WithId("Player").AsSingle();
            #region Abilities

            Container.Bind<AbilitySignals>().AsSingle();

            ShieldAbility shieldAbility = new ShieldAbility(shieldParticle);
            Container.BindInterfacesTo(shieldAbility.GetType()).FromInstance(shieldAbility);
            Container.QueueForInject(shieldAbility);

            StaminaAbility staminaAbility = new StaminaAbility(staminaParticle);
            Container.BindInterfacesTo(staminaAbility.GetType()).FromInstance(staminaAbility);
            Container.QueueForInject(staminaAbility);

            PoisonAbility poisonAbility = new PoisonAbility(poisonParticle, poisonParticleCollider);
            Container.BindInterfacesTo(poisonAbility.GetType()).FromInstance(poisonAbility);
            Container.QueueForInject(poisonAbility);

            DashAbility dashAbility = new DashAbility(dashParticle);
            Container.BindInterfacesTo(dashAbility.GetType()).FromInstance(dashAbility);
            Container.QueueForInject(dashAbility);

            AbilityManager abilityManager = new AbilityManager(shieldAbility, staminaAbility, poisonAbility, dashAbility);
            Container.BindInstance(abilityManager).AsSingle();
            Container.BindInterfacesTo<AbilityManager>().FromResolve();
            Container.QueueForInject(abilityManager);
            #endregion
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("Data/MetaData/BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();

            _enemySpawnSettings = Resources.Load<CD_EnemySpawn>("Data/MetaData/EnemySpawnSettings");
            Container.BindInstance(_enemySpawnSettings).AsSingle();

            _abilitySettings = Resources.Load<AbilitySettings>("Data/MetaData/AbilitySettings");
            Container.BindInstance(_abilitySettings).AsSingle();
        }
    }
}
