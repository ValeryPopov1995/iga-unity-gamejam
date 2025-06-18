using AncestralPotatoes.States;
using UnityEngine;

namespace AncestralPotatoes.Enemies.Behaviour
{
    public class MeleeAttackingState : EnemyState
    {
        public float AttackCooldown { get; set; }

        public MeleeAttackingState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public override void Enter()
        {
            AttackCooldown = 0f;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            if (!Context.IsCloseEnoughToPlayer())
            {
                Context.StateMachine.GoTo(Context.ClosingUp);
                return;
            }

            var enemy = Context.Enemy;

            var playerPos = Context.Player.transform.position;
            var enemyPos = enemy.transform.position;

            if ((playerPos - enemyPos).magnitude > enemy.InteractionDistance)
            {
                StateMachine.GoTo(Context.Approach);
                return;
            }

            if (AttackCooldown <= 0f)
            {
                enemy.ExecuteMeleeAttack();
                AttackCooldown = enemy.MeleeAttackCooldown;
            }
            AttackCooldown -= Time.deltaTime;
        }
    }
}