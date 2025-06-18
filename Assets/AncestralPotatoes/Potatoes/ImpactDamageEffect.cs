using UnityEngine;

namespace AncestralPotatoes.Potatoes
{
    public class ImpactDamageEffect : MonoBehaviour, IPotatoEffect
    {
        public float MinDamage = 3f;
        public float MaxDamage = 50f;

        public float MinSpeed = 1f;
        public float SpeedDamageCoefficient = 0.5f;

        public void Apply(Collision collision)
        {
            var damageReceiver = collision.collider.GetComponent<IDamageReceiver>();
            if (damageReceiver == null)
                return;
            var damage = CreateDamageDescription(collision);
            damageReceiver.ReceiveDamage(damage);
        }

        private DamageDescription CreateDamageDescription(Collision collision)
        {
            var speed = collision.relativeVelocity.magnitude;

            var point = collision.contactCount > 0 ? collision.contacts[0].point : default;
            var force = collision.contactCount > 0 ? collision.contacts[0].impulse : default;
            var damage = new DamageDescription()
            {
                Point = point,
                Force = force,
                Type = EDamageType.Impact
            };
            if (speed < MinSpeed)
                return damage;

            damage.Amount = Mathf.Clamp(MinDamage + (speed - MinSpeed) * SpeedDamageCoefficient, MinDamage, MaxDamage);
            return damage;
        }
    }
}