using AncestralPotatoes.Character;
using AncestralPotatoes.Enemies;
using AncestralPotatoes.Enemies.Behaviour;
using AncestralPotatoes.States;

public class EnemyStateContext
{
    public EnemyStateContext(Enemy enemy, Player player, IStateMachine stateMachine)
    {
        Approach = new ApproachingState(this, stateMachine);
        MeleeAttack = new MeleeAttackingState(this, stateMachine);
        RangedAttack = new RangedAttackingState(this, stateMachine);
        Enemy = enemy;
        Player = player;
        StateMachine = stateMachine;
    }

    public IState Approach { get; }
    public IState MeleeAttack { get; }
    public IState RangedAttack { get; }

    public Enemy Enemy { get; }
    public Player Player { get; }
    public IStateMachine StateMachine { get; }
}
