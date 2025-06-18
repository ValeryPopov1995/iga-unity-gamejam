using AncestralPotatoes.Potatoes;
using AncestralPotatoes.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PotatoInventory : MonoBehaviour, IPotatoInventory
    {
        private readonly List<Potato> potatoes = new();

        [field: SerializeField] public int MaxPotatoCount { get; private set; } = 10;
        public int PotatoCount => potatoes.Count;

        public event Action<Potato> OnPotatoAdded;
        public event Action<Potato> OnPotatoRemoved;

        [SerializeField] private AudioClip changeInventoryClip;
        [Inject] private readonly SfxPlayer sxfPlayer;

        public void AddPotatos(List<Potato> potatoes)
        {
            for (var i = 0; i < potatoes.Count(); i++)
                TryAddPotato(potatoes[i]);
        }

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
            sxfPlayer.PlayOneShot(changeInventoryClip);
            return true;
        }

        public bool TryAddPotato(Potato potato)
        {
            if (potatoes.Count == MaxPotatoCount) return false;
            potatoes.Add(potato);
#if UNITY_EDITOR
            Debug.Log("Potato added", this);
#endif
            OnPotatoAdded?.Invoke(potato);
            sxfPlayer.PlayOneShot(changeInventoryClip);
            return true;
        }
    }
}