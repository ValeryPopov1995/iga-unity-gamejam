using UnityEngine;

namespace AncestralPotatoes.Enemies
{
    public interface IEnemyFactory
    {
        public Enemy CreateEnemy(Vector3 position);
    }
}