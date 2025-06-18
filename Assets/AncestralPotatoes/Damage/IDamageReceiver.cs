using UnityEngine;

namespace AncestralPotatoes
{
    public interface IDamageReceiver
    {
        public void ReceiveDamage(DamageDescription damage);
        public Vector3 GetPosition();
    }
}