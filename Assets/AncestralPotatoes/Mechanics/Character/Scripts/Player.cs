using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class Player : MonoBehaviour, IDamageReceiver
    {
        public float Health { get; private set; }
        public PotatoInventory Inventory { get; private set; }
        public PlayerHand Hand { get; private set; }

        public void ReceiveDamage(DamageDescription damage)
        {
            Health -= damage.Amount;
        }

        private void Awake()
        {
            Inventory = GetComponentInChildren<PotatoInventory>();
            Hand = GetComponentInChildren<PlayerHand>();
        }
    }
}