using AncestralPotatoes.States;

public class RangedAttackingState : EnemyState
{
    public RangedAttackingState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
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
        var inventory = Context.Enemy.PotatoInventory;

    }
}