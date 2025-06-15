using AncestralPotatoes.Character;
using Zenject;

namespace AncestralPotatoes.Scene
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerCamera>().FromComponentInHierarchy().AsSingle();
        }
    }
}