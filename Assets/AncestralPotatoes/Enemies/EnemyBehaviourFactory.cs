using AncestralPotatoes.Character;
using AncestralPotatoes.States;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyBehaviourFactory : MonoBehaviour, IEnemyBehaviourFactory
    {
        [Inject] protected Player Player { get; set; }
        public EnemyStateContext CreateEnemyStateContext(Enemy enemy)
        {
            var sm = new StateMachine();
            return new EnemyStateContext(enemy, Player, sm);
        }
    }
}