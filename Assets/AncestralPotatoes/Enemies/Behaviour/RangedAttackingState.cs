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
        if (!Context.IsCloseEnoughToPlayer())
        {
            Context.StateMachine.GoTo(Context.ClosingUp);
            return;
        }
        var enemy = Context.Enemy;
        var player = Context.Player;

        var distance = (enemy.transform.position - player.transform.position).magnitude;

        var inventory = enemy.PotatoInventory;
        if (inventory.PotatoCount == 0 || distance > enemy.RangedAttackDistance)
        {
            Context.StateMachine.GoTo(Context.Approach);
            return;
        }

        if (AttackCooldown <= 0f)
        {
            enemy.SetTargetPosition(enemy.transform.position);
            enemy.Hand.transform.LookAt(player.transform.position, Vector3.up);

            //var tweener = enemy.transform.DOLookAt(player.transform.position, 0.2f, up: Vector3.up);
            enemy.ExecuteRangedAttack();
            AttackCooldown = enemy.RangedAttackCooldown;
        }
        AttackCooldown -= Time.deltaTime;
    }
}