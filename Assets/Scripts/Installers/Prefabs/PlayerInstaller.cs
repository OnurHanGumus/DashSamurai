using UnityEngine;
using Zenject;

namespace Installers.Prefabs
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        //private PlayerSettings _playerSettings; //Ust installere tasindi.
        
        public override void InstallBindings()
        {
            //_playerSettings = Resources.Load<PlayerSettings>("Data/MetaData/PlayerSettings");

            //Container.BindInstance(_playerSettings).AsSingle();
            
        }
    }
}
