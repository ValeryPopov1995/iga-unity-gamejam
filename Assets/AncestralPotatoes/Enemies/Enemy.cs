using AncestralPotatoes.Character;
using AncestralPotatoes.States;
using System;
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

        [Inject] private readonly Player player;
        public IPotatoInventory PotatoInventory { get; protected set; }
        public ThrowingHand Hand { get; protected set; }
        [field: SerializeField] public EEnemyType Type { get; protected set; }

        [field: SerializeField] public float Health { get; private set; } = 30f;
        [field: SerializeField] public float InteractionDistance { get; set; } = 2f;
        [field: SerializeField] public float MeleeAttackCooldown { get; set; } = 1.0f;
        [field: SerializeField] public float MeleeAttackDamage { get; set; } = 3.0f;
        [field: SerializeField] public float RangedAttackDistance { get; set; } = 7f;
        [field: SerializeField] public float RangedAttackCooldown { get; set; } = 2.0f;
        [field: SerializeField] public float ActivityRangeFromPlayer { get; set; } = 30f;
        [field: SerializeField] public float FrightDistance { get; set; } = 0f;

        public event Action<Enemy> OnDeath;

        public static IState GetStartingState(EnemyStateContext context)
        {
            switch (context.Enemy.Type)
            {
                case EEnemyType.Fighter:
                    return context.Approach;
                case EEnemyType.Support:
                    return context.Gathering;
                default:
                    return context.Approach;
            }
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.stoppingDistance = InteractionDistance * 0.9f;
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
            stateMachine.Initialize(GetStartingState(context));
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
                Gizmos.DrawSphere(transform.position, InteractionDistance);
            }
        }

        public void ReceiveDamage(DamageDescription damage)
        {
            Health -= damage.Amount;
            if (Health < 0)
            {
                OnDeath?.Invoke(this);
                Destroy(this);
            }
        }

        public void ExecuteMeleeAttack()
        {
            if ((player.transform.position - transform.position).magnitude < InteractionDistance)
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