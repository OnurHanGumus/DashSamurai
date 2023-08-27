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
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private Transform playerTransform;
        private BulletSettings _bulletSettings;
        private EnemySpawnSettings _enemySpawnSettings;

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

            //Container.Bind<DenemeController>().AsSingle();
            //Container.BindInterfacesTo<DenemeController>().with.FromResolve();
            //Container.Bind<IDeneme>().WithId("DenemeController").To<DenemeController>().AsSingle();
            //Container.Bind<IDeneme>().WithId("DenemeController2").To<DenemeController2>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemySpawnManager>().AsSingle();
            Container.BindInstance(playerTransform).WithId("Player").AsSingle();
        }

        private void BindSettings()
        {
            _bulletSettings = Resources.Load<BulletSettings>("Data/MetaData/BulletSettings");
            Container.BindInstance(_bulletSettings).AsSingle();

            _enemySpawnSettings = Resources.Load<EnemySpawnSettings>("Data/MetaData/EnemySpawnSettings");
            Container.BindInstance(_enemySpawnSettings).AsSingle();
        }
    }
}
