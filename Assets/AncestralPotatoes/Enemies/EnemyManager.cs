using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyManager : MonoBehaviour, IEnemyLocator, IEnemySpawner
    {
        [Inject] private readonly IEnemyFactory _enemyFactory;
        [Inject] private readonly IEnemyBehaviourFactory _enemyBehaviourFactory;
        private readonly List<Enemy> fighterList = new();

        public Enemy GetClosestFighter(Vector3 position)
        {
            return fighterList.OrderBy(f => Vector3.Distance(position, f.transform.position)).FirstOrDefault();
        }

        public Enemy SpawnFighter(Vector3 position)
        {
            var enemy = _enemyFactory.CreateFighter(position);
            enemy.OnDeath += RemoveFighter;
            fighterList.Add(enemy);
            var context = _enemyBehaviourFactory.CreateEnemyStateContext(enemy);
            enemy.StartBehaviour(context);
            return enemy;
        }

        public Enemy SpawnSupport(Vector3 position)
        {
            var enemy = _enemyFactory.CreateSupport(position);
            var context = _enemyBehaviourFactory.CreateEnemyStateContext(enemy);
            enemy.StartBehaviour(context);
            return enemy;
        }

        private void RemoveFighter(Enemy fighter)
        {
            fighter.OnDeath -= RemoveFighter;
            fighterList.Remove(fighter);
        }
    }
}