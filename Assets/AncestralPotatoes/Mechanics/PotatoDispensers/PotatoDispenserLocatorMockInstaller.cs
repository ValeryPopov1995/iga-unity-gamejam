using Zenject;

namespace AncestralPotatoes.Mechanics.PotatoDispensers
{
    public class PotatoDispenserLocatorMockInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPotatoDispenserLocator>().To<PotatoDispenserLocatorMock>().FromComponentOn(gameObject).AsSingle();
        }
    }
}
