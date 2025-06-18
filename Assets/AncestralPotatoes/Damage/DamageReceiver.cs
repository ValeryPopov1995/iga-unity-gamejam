using UnityEngine;

namespace AncestralPotatoes
{
    public class DamageReceiver : MonoBehaviour, IDamageReceiver
    {
        public float Health = 10f;

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void ReceiveDamage(DamageDescription damage)
        {
            switch (damage.Type)
            {
                case EDamageType.Impact:
                    Debug.Log($"Received {damage.Amount} damage");
                    Health -= damage.Amount;
                    if (Health < 0)
                        Destroy(gameObject);
                    break;
            }
        }
    }
}