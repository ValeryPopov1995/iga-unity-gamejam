using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [Inject] private readonly IEnemyFactory _enemyFactory;
        [Inject] private readonly IEnemyBehaviourFactory _enemyBehaviourFactory;
        public void CreateEnemy(Vector3 position)
        {
            var enemy = _enemyFactory.CreateEnemy(position);
            var context = _enemyBehaviourFactory.CreateEnemyStateContext(enemy);
            enemy.StartBehaviour(context);
        }
    }
}