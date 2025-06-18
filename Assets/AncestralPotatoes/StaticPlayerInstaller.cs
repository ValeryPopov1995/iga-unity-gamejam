using AncestralPotatoes.Character;
using Zenject;

namespace AncestralPotatoes
{
    public class StaticPlayerInstaller : Installer<StaticPlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}