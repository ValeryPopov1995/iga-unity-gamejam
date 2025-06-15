using AncestralPotatoes.Character;
using AncestralPotatoes.States;
using UnityEngine;
using UnityEngine.AI;

namespace AncestralPotatoes.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private NavMeshAgent agent;
        private IStateMachine stateMachine;
        public PotatoInventory PotatoInventory { get; protected set; }

        [field: SerializeField] public float MeleeAttackRange { get; set; } = 2f;
        [field: SerializeField] public float MeleeCooldown { get; set; } = 1.0f;
        [field: SerializeField] public float RangedAttackRange { get; set; } = 7f;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = MeleeAttackRange * 0.9f;
            PotatoInventory = GetComponent<PotatoInventory>();
        }

        private void Update()
        {
            if (stateMachine != null)
                stateMachine.Update();
        }

        public void SetTargetPosition(Vector3 targetPos)
        {
            agent.SetDestination(targetPos);
        }

        public void StartBehaviour(EnemyStateContext context)
        {
            stateMachine = context.StateMachine;
            stateMachine.Initialize(context.Approach);
        }

        internal void ExecuteMeleeAttack()
        {
            Debug.Log("Attack");
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                var path = agent.path;
                foreach (var corner in path.corners)
                {
                    Gizmos.DrawCube(corner, Vector3.one * 0.3f);
                }
                Gizmos.color = new Color(1, 0, 0, 0.3f);
                Gizmos.DrawSphere(transform.position, MeleeAttackRange);
            }
        }
    }
}