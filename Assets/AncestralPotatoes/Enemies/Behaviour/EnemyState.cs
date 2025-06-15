using AncestralPotatoes.States;

public abstract class EnemyState : StateBase
{
    protected EnemyStateContext Context { get; set; }

    public EnemyState(EnemyStateContext context, IStateMachine stateMachine) : base(stateMachine)
    {
        Context = context;
    }
}
