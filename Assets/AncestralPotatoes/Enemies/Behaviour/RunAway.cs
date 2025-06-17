using AncestralPotatoes.Enemies;
using AncestralPotatoes.States;
using UnityEngine;

internal class RunAway : EnemyState
{
    private Vector3 _targetPos;

    public RunAway(EnemyStateContext enemyStateContext, IStateMachine stateMachine) : base(enemyStateContext, stateMachine)
    {
    }

    public override void Enter()
    {
        var enemy = Context.Enemy;
        var enemyPos = enemy.transform.position;
        var direction = (enemyPos - Context.Player.transform.position).normalized;
        _targetPos = enemyPos + 0.8f * enemy.ActivityRangeFromPlayer * direction;
        enemy.SetTargetPosition(_targetPos);
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

        if ((Context.Enemy.transform.position - _targetPos).magnitude < Context.Enemy.InteractionDistance)
        {
            Context.StateMachine.GoTo(Enemy.GetStartingState(Context));
            return;
        }

    }
}