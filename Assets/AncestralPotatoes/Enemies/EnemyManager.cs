using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        [Inject] private IEnemyFactory _enemyFactory;
        
    }

    public interface IEnemyFactory
    {
        
    }
}