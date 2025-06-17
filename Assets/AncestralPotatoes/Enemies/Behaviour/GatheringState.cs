using AncestralPotatoes.Mechanics.PotatoDispensers;
using AncestralPotatoes.States;

public class GatheringState : EnemyState
{
    private IPotatoDispenser dispenser;

    public GatheringState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
    {
    }

    public override void Enter()
    {
        var enemy = Context.Enemy;
        dispenser = Context.DispenserLocator.GetClosestDispenserWithPotato(enemy.transform.position);
        enemy.SetTargetPosition(dispenser.GetPosition());
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

        // is player too close
        if (Context.IsTooCloseToPlayer())
        {
            Context.StateMachine.GoTo(Context.RunAway);
            return;
        }

        var enemy = Context.Enemy;

        if ((enemy.transform.position - dispenser.GetPosition()).magnitude > enemy.InteractionDistance)
            return;

        var isTherePotato = dispenser.TryGetPotato(out var potato);
        if (!isTherePotato)
        {
            Context.StateMachine.GoTo(Context.Gathering); // potato probably was collected by someone else, looking for next
            return;
        }

        enemy.PotatoInventory.TryAddPotato(potato);
        Context.StateMachine.GoTo(Context.Giving);
    }
}
