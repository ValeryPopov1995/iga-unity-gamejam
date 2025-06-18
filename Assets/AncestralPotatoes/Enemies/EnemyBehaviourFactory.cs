using AncestralPotatoes.Character;
using AncestralPotatoes.Mechanics.PotatoDispensers;
using AncestralPotatoes.States;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyBehaviourFactory : MonoBehaviour, IEnemyBehaviourFactory
    {
        [Inject] private readonly DiContainer container;
        public EnemyStateContext CreateEnemyStateContext(Enemy enemy)
        {
            var sm = new StateMachine();
            return new EnemyStateContext(enemy,
                                         container.Resolve<Player>(),
                                         container.Resolve<IPotatoDispenserLocator>(),
                                         sm,
                                         container.Resolve<IEnemyLocator>());
        }
    }
}