using AncestralPotatoes.States;
using UnityEngine;
using UnityEngine.AI;

namespace AncestralPotatoes.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private IStateMachine _stateMachine;

        [field: SerializeField] public float MeleeRange { get; set; } = 1f;
        [field: SerializeField] public float MeleeCooldown { get; set; } = 1.0f;
        [field: SerializeField] public float PotatoThrowRange { get; set; } = 4f;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_stateMachine != null)
                _stateMachine.Update();
        }

        public void SetTargetPosition(Vector3 targetPos)
        {
            _agent.SetDestination(targetPos);
        }

        public void StartBehaviour(EnemyStateContext context)
        {
            _stateMachine = context.StateMachine;
            _stateMachine.Initialize(context.Approach);
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
                Gizmos.color = new Color(1, 0, 0, 0.3f);
                Gizmos.DrawSphere(transform.position, MeleeRange);
            }
        }

        internal void ExecuteMeleeAttack()
        {
            Debug.Log("Attack");
        }
    }
}