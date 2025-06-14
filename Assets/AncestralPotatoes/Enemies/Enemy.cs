using UnityEngine;
using UnityEngine.AI;

namespace AncestralPotatoes.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
    }

    public class DebugEnemyController : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private void Update()
        {

        }
    }
}