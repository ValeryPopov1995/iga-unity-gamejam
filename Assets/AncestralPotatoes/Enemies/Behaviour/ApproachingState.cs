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
            var enemyPos = Context.Enemy.transform.position;

            if ((enemyPos - playerPos).magnitude < Context.Enemy.MeleeAttackRange)
                StateMachine.GoTo(Context.MeleeAttack);


            Context.Enemy.SetTargetPosition(playerPos);
        }
    }

}