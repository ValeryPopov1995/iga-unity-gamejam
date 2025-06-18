public static class GlobalStats
{
    public static int EnemiesDefeated { get; private set; } = 0;
    public static void IncrementDefeatedEnemies() => EnemiesDefeated++;
    public static int PotatoesCollected { get; private set; } = 0;
    public static void IncrementCollectedPotatoes() => PotatoesCollected++;
    public static int DangerLevel { get; private set; }
    public static void IncrementDangerLevel() => DangerLevel++;

    public static int FightersOnLevel { get; set; }
    public static int MaxFightersOnLevel => 5 + DangerLevel * 4;
    public static int SupportsOnLevel { get; set; }
    public static int MaxSupportsOnLevel => 3 + DangerLevel * 2;
}