using AncestralPotatoes.Character;
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
            Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerCamera>().FromComponentInHierarchy().AsSingle();

            Container.Bind<IEnemyFactory>().To<EnemyFactory>().FromInstance(enemyFactory).AsSingle();
            Container.Bind<IEnemyBehaviourFactory>().To<EnemyBehaviourFactory>().FromInstance(enemyBehaviourFactory).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyManager>().FromComponentInHierarchy().AsSingle();

            Container.Bind<SfxPlayer>().FromInstance(SfxPlayer.Create()).AsSingle();
        }
    }
}