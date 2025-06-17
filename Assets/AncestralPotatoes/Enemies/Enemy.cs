using AncestralPotatoes.Character;
using AncestralPotatoes.States;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour, IDamageReceiver
    {
        private NavMeshAgent agent;
        private IStateMachine stateMachine;
        public IPotatoInventory PotatoInventory { get; protected set; }
        public ThrowingHand Hand { get; protected set; }

        [Inject] private readonly Player player;

        [field: SerializeField] public float Health { get; private set; } = 30f;
        [field: SerializeField] public float MeleeAttackDistance { get; set; } = 2f;
        [field: SerializeField] public float MeleeAttackCooldown { get; set; } = 1.0f;
        [field: SerializeField] public float MeleeAttackDamage { get; set; } = 3.0f;
        [field: SerializeField] public float RangedAttackDistance { get; set; } = 7f;
        [field: SerializeField] public float RangedAttackCooldown { get; set; } = 2.0f;


        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = MeleeAttackDistance * 0.9f;
            PotatoInventory = GetComponent<PotatoInventory>();
            Hand = GetComponent<ThrowingHand>();
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
                Gizmos.DrawSphere(transform.position, MeleeAttackDistance);
            }
        }

        public void ReceiveDamage(DamageDescription damage)
        {
            Health -= damage.Amount;
        }

        public void ExecuteMeleeAttack()
        {
            if ((player.transform.position - transform.position).magnitude < MeleeAttackDistance)
            {
                var damage = new DamageDescription()
                {
                    Type = EDamageType.Impact,
                    Amount = MeleeAttackDamage
                };
                player.ReceiveDamage(damage);
            }
        }

        public void ExecuteRangedAttack()
        {
            Debug.Log("Throwing potato");
            Hand.SelectPotato();
            Hand.ThrowPotato(1f);
        }
    }
}