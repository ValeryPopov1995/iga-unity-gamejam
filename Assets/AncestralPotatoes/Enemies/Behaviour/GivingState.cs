using AncestralPotatoes.Enemies;
using AncestralPotatoes.States;
using UnityEngine;

public class GivingState : EnemyState
{
    private Enemy fighter;

    public GivingState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
    {
    }

    public override void Enter()
    {
        fighter = Context.EnemyLocator.GetClosestFighter(Context.Enemy.transform.position);
        if (fighter == null)
        {
            Context.StateMachine.GoTo(Context.Gathering);
            return;
        }
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

        if (fighter == null)
        {
            Context.StateMachine.GoTo(Context.Gathering);
            return;
        }

        var enemy = Context.Enemy;

        enemy.SetTargetPosition(fighter.transform.position);

        if (Vector3.Distance(enemy.transform.position, fighter.transform.position) >= enemy.InteractionDistance)
            return;

        var eI = enemy.PotatoInventory;
        if (!eI.TryGetRandomPotato(out var potato))
        {
            Context.StateMachine.GoTo(Context.Gathering);
            return;
        }

        var fI = fighter.PotatoInventory;
        fI.TryAddPotato(potato);
        if (eI.PotatoCount > 0)
            return;
        Context.StateMachine.GoTo(Context.Gathering);
    }
}
