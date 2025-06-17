using AncestralPotatoes.Enemies;
using AncestralPotatoes.States;

public class ClosingUpState : EnemyState
{

    public ClosingUpState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
    {
    }

    public override void Enter()
    {
        Context.Enemy.SetTargetPosition(Context.Player.transform.position);
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        var enemy = Context.Enemy;
        if (Context.DistanceToPlayer() < enemy.ActivityRangeFromPlayer)
        {
            Context.StateMachine.GoTo(Enemy.GetStartingState(Context));
            return;
        }
    }
}