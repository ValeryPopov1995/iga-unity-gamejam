using AncestralPotatoes.States;

namespace AncestralPotatoes.Enemies.Behaviour
{
    public class ApproachingState : EnemyState
    {
        public ApproachingState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            var playerPos = Context.Player.transform.position;
            var enemy = Context.Enemy;
            var enemyPos = enemy.transform.position;

            var distance = (enemyPos - playerPos).magnitude;

            if (distance < enemy.MeleeAttackDistance)
                StateMachine.GoTo(Context.MeleeAttack);

            if (distance < enemy.RangedAttackDistance && enemy.PotatoInventory.PotatoCount > 0)
                StateMachine.GoTo(Context.RangedAttack);

            Context.Enemy.SetTargetPosition(playerPos);
        }
    }

}