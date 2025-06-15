using AncestralPotatoes;
using AncestralPotatoes.Enemies;
using AncestralPotatoes.Enemies.Behaviour;
using AncestralPotatoes.States;

public class EnemyStateContext
{
    public EnemyStateContext(Enemy enemy, IPlayerLocator playerLocator, IStateMachine stateMachine)
    {
        Approach = new ApproachingState(this, stateMachine);
        Attack = new MeleeAttackingState(this, stateMachine);
        Enemy = enemy;
        PlayerLocator = playerLocator;
        StateMachine = stateMachine;
    }

    public IState Approach { get; }
    public IState Attack { get; }

    public Enemy Enemy { get; }
    public IPlayerLocator PlayerLocator { get; }
    public IStateMachine StateMachine { get; }
}
