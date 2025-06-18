using AncestralPotatoes.Enemies;
using AncestralPotatoes.States;

public class ClosingUpState : EnemyState
{

    public ClosingUpState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
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
        Enemy enemy = Context.Enemy;
        if (Context.DistanceToPlayer() < enemy.ActivityRangeFromPlayer)
        {
            Context.StateMachine.GoTo(Enemy.GetStartingState(Context));
            return;
        }
        enemy.SetTargetPosition(Context.Player.transform.position);
    }
}