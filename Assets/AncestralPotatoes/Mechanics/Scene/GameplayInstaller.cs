using AncestralPotatoes.Enemies;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Scene
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private EnemyFactory enemyFactory;
        [SerializeField] private EnemyBehaviourFactory enemyBehaviourFactory;

        public override void InstallBindings()
        {
            //Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            //Container.Bind<PlayerCamera>().FromComponentInHierarchy().AsSingle();
            PlayerInstaller.Install(Container);

            Container.Bind<IEnemyFactory>().FromInstance(enemyFactory).AsSingle();
            Container.Bind<IEnemyBehaviourFactory>().FromInstance(enemyBehaviourFactory).AsSingle();
            Container.Bind<EnemyManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}