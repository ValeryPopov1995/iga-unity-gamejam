namespace AncestralPotatoes.Enemies
{
    public interface IEnemyBehaviourFactory
    {
        public EnemyStateContext CreateEnemyStateContext(Enemy enemy);
    }
}