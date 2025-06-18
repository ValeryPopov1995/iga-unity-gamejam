using UnityEngine;

namespace AncestralPotatoes.Potatoes
{
    public class ExplosionDamageEffect : MonoBehaviour, IPotatoEffect
    {
        public float MinDamage = 20f;
        public float MaxDamage = 50f;
        public float Radius = 15f;
        public bool hasExploded = false;

        public void Apply(Collision collision)
        {
            if (hasExploded)
                return;
            if (collision.contactCount == 0)
                return;
            var contact = collision.contacts[0];

            var point = contact.point;

            var colliders = Physics.OverlapSphere(point, Radius);
            foreach (var collider in colliders)
            {
                var damageReceiver = collider.GetComponentInParent<IDamageReceiver>();
                if (damageReceiver == null)
                    continue;
                var pos = damageReceiver.GetPosition();
                var force = pos - point;
                var damage = CreateDamageDescription(point, force);
                damageReceiver.ReceiveDamage(damage);
            }
            hasExploded = true;
        }

        private DamageDescription CreateDamageDescription(Vector3 point, Vector3 force)
        {
            var amount = Mathf.Lerp(MaxDamage, MinDamage, force.magnitude / Radius);
            var damage = new DamageDescription()
            {
                Type = EDamageType.Impact,
                Amount = amount,
                Point = point,
                Force = -force
            };

            return damage;
        }
    }
}