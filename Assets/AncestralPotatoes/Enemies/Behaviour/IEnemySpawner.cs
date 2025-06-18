
using AncestralPotatoes.Enemies;
using UnityEngine;

public interface IEnemySpawner
{
    public Enemy SpawnFighter(Vector3 position);
    public Enemy SpawnSupport(Vector3 position);
}