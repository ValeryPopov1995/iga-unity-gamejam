using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<IEnemyBehaviourFactory>().To<EnemyBehaviourFactory>().FromComponentOn(gameObject).AsSingle();
            Container.Bind<EnemyManager>().FromComponentOn(gameObject).AsSingle();
        }
    }
}