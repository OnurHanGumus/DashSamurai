using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.Prefabs
{
    public class MagicInstaller : MonoInstaller<MagicInstaller>
    {
        private EnemySettings _wizardSettings;

        public override void InstallBindings()
        {
            _wizardSettings = Resources.Load<EnemySettings>("Data/MetaData/WizardSettings");
            Container.BindInstance(_wizardSettings).AsSingle();

        }
    }
}
