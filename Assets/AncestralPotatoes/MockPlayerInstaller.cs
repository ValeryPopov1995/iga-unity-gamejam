using Zenject;

namespace AncestralPotatoes
{
    public class MockPlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            PlayerInstaller.Install(Container);
        }
    }
}