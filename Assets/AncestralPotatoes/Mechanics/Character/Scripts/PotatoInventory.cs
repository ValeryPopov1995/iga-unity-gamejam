using AncestralPotatoes.Potatoes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class PotatoInventory : MonoBehaviour
    {
        internal event Action<Potato> OnAdd, OnRemove;
        internal Potato RandomNext => potatoes.Count > 0 ? potatoes[UnityEngine.Random.Range(0, potatoes.Count - 1)] : null;

        [field: SerializeField] public int MaxCouunt { get; private set; } = 10;
        private readonly List<Potato> potatoes = new();

        public bool TryAddPotato(Potato potato)
        {
            if (potatoes.Count == MaxCouunt || potato == null) return false;
            potatoes.Add(potato);
#if UNITY_EDITOR
            Debug.Log("potato added", this);
#endif
            OnAdd?.Invoke(potato);
            return true;
        }

        public bool TryRemovePotato(Potato potato)
        {
            if (!potatoes.Contains(potato) || potato == null) return false;
            potatoes.Remove(potato);
#if UNITY_EDITOR
            Debug.Log("potato removed", this);
#endif
            OnRemove?.Invoke(potato);
            return true;
        }
    }
}