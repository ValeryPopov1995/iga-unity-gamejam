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
            var speed = collision.relativeVelocity.magnitude;
            var damage = CreateDamageDescription(speed);
            damageReceiver.ReceiveDamage(damage);
        }

        private DamageDescription CreateDamageDescription(float speed)
        {
            var damage = new DamageDescription()
            {
                Type = EDamageType.Impact
            };
            if (speed < MinSpeed)
                return damage;

            damage.Amount = Mathf.Clamp(MinDamage + (speed - MinSpeed) * SpeedDamageCoefficient, MinDamage, MaxDamage);
            return damage;
        }
    }
}