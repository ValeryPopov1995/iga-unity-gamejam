using AncestralPotatoes.Character;
using UnityEngine;

namespace AncestralPotatoes.Cart
{
    public class Cart : MonoBehaviour
    {
        public PotatoInventory Inventory { get; private set; } = new();
    }
}