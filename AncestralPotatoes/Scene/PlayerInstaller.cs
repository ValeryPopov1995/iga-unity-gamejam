using AncestralPotatoes.Character;
using Zenject;

namespace AncestralPotatoes.Scene
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}