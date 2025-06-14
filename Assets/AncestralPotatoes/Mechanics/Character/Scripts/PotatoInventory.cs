using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class PotatoInventory : MonoBehaviour
    {
        internal PotatoDiscriptionMockup RandomNext => potatoes.Count > 0 ? potatoes[Random.Range(0, potatoes.Count - 1)] : null;

        [field: SerializeField] public int MaxCouunt { get; private set; } = 10;
        private readonly List<PotatoDiscriptionMockup> potatoes = new();

        public bool TryAddPotato(PotatoDiscriptionMockup potato)
        {
            if (potatoes.Count == MaxCouunt || potato == null) return false;
            potatoes.Add(potato);
            return true;
        }

        public bool TryRemovePotato(PotatoDiscriptionMockup potato)
        {
            if (!potatoes.Contains(potato) || potato == null) return false;
            potatoes.Remove(potato);
            return true;
        }

    }
}