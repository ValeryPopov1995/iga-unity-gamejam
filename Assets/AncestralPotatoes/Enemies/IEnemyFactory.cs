using UnityEngine;

namespace AncestralPotatoes.Enemies
{
    public interface IEnemyFactory
    {
        public Enemy CreateFighter(Vector3 position);
        public Enemy CreateSupport(Vector3 position);
    }
}