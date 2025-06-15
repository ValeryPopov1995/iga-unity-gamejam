using UnityEngine;

namespace AncestralPotatoes
{
    public class DamageReceiver : MonoBehaviour, IDamageReceiver
    {
        public float Health = 10f;

        public void ReceiveDamage(DamageDescription damage)
        {
            switch (damage.Type)
            {
                case EDamageType.Impact:
                    Debug.Log($"Received {damage.Damage} damage");
                    Health -= damage.Damage;
                    if (Health < 0)
                        Destroy(gameObject);
                    break;
            }
        }
    }
}