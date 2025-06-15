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
            var enemy = Context.Enemy;

            var playerPos = Context.Player.transform.position;
            var enemyPos = enemy.transform.position;

            if ((playerPos - enemyPos).magnitude > enemy.MeleeRange)
                StateMachine.GoTo(Context.Approach);

            if (AttackCooldown <= 0f)
            {
                enemy.ExecuteMeleeAttack();
                AttackCooldown = enemy.MeleeCooldown;
            }
            AttackCooldown -= Time.deltaTime;
        }
    }
}