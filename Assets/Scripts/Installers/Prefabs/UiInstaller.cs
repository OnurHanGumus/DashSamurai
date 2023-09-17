using Data.MetaData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Installers.Prefabs
{
    public class UiInstaller : MonoInstaller<UiInstaller>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI sliderText;
        [SerializeField] private Image staminaSliderImage;
        public override void InstallBindings()
        {
            Container.Bind<SliderIncreaseAutomatically>().AsSingle();
            Container.BindInstance(slider).AsSingle();
            Container.BindInstance(sliderText).AsSingle();
            Container.BindInstance(staminaSliderImage).WithId("StaminaFill").AsSingle();
        }
    }
}
