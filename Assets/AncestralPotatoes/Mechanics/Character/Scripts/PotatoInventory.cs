using AncestralPotatoes.Potatoes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class PotatoInventory : MonoBehaviour, IPotatoInventory
    {
        private readonly List<Potato> potatoes = new();

        public int PotatoCount => potatoes.Count;
        [field: SerializeField] public int MaxPotatoCount { get; private set; } = 10;

        public event Action<Potato> OnPotatoAdded;
        public event Action<Potato> OnPotatoRemoved;

        public bool TryGetRandomPotato(out Potato potato)
        {
            if (potatoes.Count == 0)
            {
                potato = null;
                return false;
            }
            var index = UnityEngine.Random.Range(0, potatoes.Count - 1);
            potato = potatoes[index];
            potatoes.RemoveAt(index);
#if UNITY_EDITOR
            Debug.Log("Potato removed", this);
#endif
            OnPotatoRemoved?.Invoke(potato);
            return true;
        }

        public bool TryAddPotato(Potato potato)
        {
            if (potatoes.Count == MaxPotatoCount || potatoes.Contains(potato))
                return false;
            potatoes.Add(potato);
#if UNITY_EDITOR
            Debug.Log("Potato added", this);
#endif
            OnPotatoAdded?.Invoke(potato);
            return true;
        }
    }
}