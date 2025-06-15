using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class Player : MonoBehaviour
    {
        public PotatoInventory Inventory { get; private set; }
        public PlayerHand Hand { get; private set; }

        private void Awake()
        {
            Inventory = GetComponentInChildren<PotatoInventory>();
            Hand = GetComponentInChildren<PlayerHand>();
        }
    }
}