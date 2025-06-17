using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class Player : MonoBehaviour, IDamageReceiver
    {
        [field: SerializeField] public float Health { get; private set; } = 30f;
        public PotatoInventory Inventory { get; private set; }
        public PlayerHand Hand { get; private set; }

        public void ReceiveDamage(DamageDescription damage)
        {
            Health -= damage.Amount;
            Debug.Log($"Received {damage.Amount} damage ({Health})");
        }

        private void Awake()
        {
            Inventory = GetComponentInChildren<PotatoInventory>();
            Hand = GetComponentInChildren<PlayerHand>();
        }
    }
}