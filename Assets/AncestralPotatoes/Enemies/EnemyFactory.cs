using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyFactory : MonoBehaviour, IEnemyFactory
    {
        [SerializeField] private Enemy _fighterPrefab;
        [SerializeField] private Enemy _supportPrefab;
        [Inject] protected DiContainer DiContainer { get; set; }

        public void Init(DiContainer diContainer)
        {
            DiContainer = diContainer;
        }

        public Enemy CreateFighter(Vector3 position)
        {
            return DiContainer.InstantiatePrefabForComponent<Enemy>(_fighterPrefab, position, Quaternion.identity, transform);
        }

        public Enemy CreateSupport(Vector3 position)
        {
            return DiContainer.InstantiatePrefabForComponent<Enemy>(_supportPrefab, position, Quaternion.identity, transform);
        }
    }
}