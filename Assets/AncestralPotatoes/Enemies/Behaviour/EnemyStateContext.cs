using AncestralPotatoes.Character;
using AncestralPotatoes.Enemies;
using AncestralPotatoes.Enemies.Behaviour;
using AncestralPotatoes.Mechanics.PotatoDispensers;
using AncestralPotatoes.States;

public class EnemyStateContext
{
    public EnemyStateContext(Enemy enemy, Player player, IPotatoDispenserLocator dispenserLocator, IStateMachine stateMachine, IEnemyLocator enemyLocator)
    {
        Approach = new ApproachingState(this, stateMachine);
        MeleeAttack = new MeleeAttackingState(this, stateMachine);
        RangedAttack = new RangedAttackingState(this, stateMachine);
        Gathering = new GatheringState(this, stateMachine);
        Giving = new GivingState(this, stateMachine);
        ClosingUp = new ClosingUpState(this, stateMachine);
        RunAway = new RunAway(this, stateMachine);
        Enemy = enemy;
        Player = player;
        DispenserLocator = dispenserLocator;
        StateMachine = stateMachine;
        EnemyLocator = enemyLocator;
    }

    public IState Approach { get; }
    public IState MeleeAttack { get; }
    public IState RangedAttack { get; }
    public IState Gathering { get; }
    public IState Giving { get; }
    public IState ClosingUp { get; }
    public IState RunAway { get; }

    public Enemy Enemy { get; }
    public Player Player { get; }
    public IPotatoDispenserLocator DispenserLocator { get; }
    public IEnemyLocator EnemyLocator { get; }
    public IStateMachine StateMachine { get; }

    public float DistanceToPlayer() => (Enemy.transform.position - Player.transform.position).magnitude;
    public bool IsCloseEnoughToPlayer() => DistanceToPlayer() < Enemy.ActivityRangeFromPlayer;
    public bool IsTooCloseToPlayer() => DistanceToPlayer() < Enemy.FrightDistance;

}
