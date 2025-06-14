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

        public void SetTargetPosition(Vector3 targetPos)
        {
            _agent.SetDestination(targetPos);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                var path = _agent.path;
                foreach (var corner in path.corners)
                {
                    Gizmos.DrawCube(corner, Vector3.one * 0.3f);
                }
            }
        }
    }
}