using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        [SerializeField] private Enemy _enemyPrefab;
        [Inject] protected DiContainer DiContainer { get; set; }

        public void Init(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }


        public Enemy CreateEnemy(Vector3 position)
        {
            var enemy = DiContainer.InstantiatePrefabForComponent<Enemy>(_enemyPrefab, position, Quaternion.identity, transform);
            return enemy;
        }
    }
}