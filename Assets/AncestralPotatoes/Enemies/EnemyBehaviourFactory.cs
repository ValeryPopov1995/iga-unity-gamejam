using AncestralPotatoes.States;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyBehaviourFactory : MonoBehaviour, IEnemyBehaviourFactory
    {
        [Inject] protected DiContainer DiContainer { get; set; }
        public EnemyStateContext CreateEnemyStateContext(Enemy enemy)
        {
            var playerLocator = DiContainer.Resolve<IPlayerLocator>();
            var sm = new StateMachine();
            return new EnemyStateContext(enemy, playerLocator, sm);
        }
    }
}