using AncestralPotatoes.States;
using UnityEngine;

public class RangedAttackingState : EnemyState
{
    public float AttackCooldown { get; set; }
    public RangedAttackingState(EnemyStateContext context, IStateMachine stateMachine) : base(context, stateMachine)
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
        var inventory = enemy.PotatoInventory;
        if (inventory.PotatoCount == 0)
            Context.StateMachine.GoTo(Context.Approach);

        if (AttackCooldown <= 0f)
        {
            enemy.ExecuteRangedAttack();
            AttackCooldown = enemy.RangedAttackCooldown;
        }
        AttackCooldown -= Time.deltaTime;
    }
}