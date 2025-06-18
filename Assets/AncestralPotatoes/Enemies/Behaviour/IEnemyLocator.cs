using AncestralPotatoes.Enemies;
using UnityEngine;

public interface IEnemyLocator
{
    public Enemy GetClosestFighter(Vector3 position);
}

//public class EnemyManager : MonoBehaviour, IEnemyLocator, IEnemySpawner
//{
//    private readonly List<Enemy> fighters = new();


//    public Enemy GetClosestFighter(Vector3 position)
//    {
//        return fighters.OrderBy(f => Vector3.Distance(position, f.transform.position)).FirstOrDefault();
//    }

//    public Enemy SpawnFighter(System.Numerics.Vector3 position)
//    {

//    }

//    public Enemy SpawnSupport(System.Numerics.Vector3 position)
//    {

//    }
//}