using AncestralPotatoes.States;
using DG.Tweening;
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
        var player = Context.Player;

        var distance = (enemy.transform.position - player.transform.position).magnitude;

        var inventory = enemy.PotatoInventory;
        if (inventory.PotatoCount == 0 || distance > enemy.RangedAttackDistance)
            Context.StateMachine.GoTo(Context.Approach);

        if (AttackCooldown <= 0f)
        {
            enemy.SetTargetPosition(enemy.transform.position);
            var tweener = enemy.transform.DOLookAt(player.transform.position, 0.2f, up: Vector3.up);
            tweener.onComplete += enemy.ExecuteRangedAttack;
            AttackCooldown = enemy.RangedAttackCooldown;
        }
        AttackCooldown -= Time.deltaTime;
    }
}