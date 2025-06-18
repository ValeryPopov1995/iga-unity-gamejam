using Zenject;

namespace AncestralPotatoes
{
    public class MockPlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            StaticPlayerInstaller.Install(Container);
        }
    }
}