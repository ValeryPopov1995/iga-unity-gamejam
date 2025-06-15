using AncestralPotatoes.Potatoes;
using System;

namespace AncestralPotatoes.Character
{
    public interface IPotatoInventory
    {
        public event Action<Potato> OnPotatoAdded, OnPotatoRemoved;
        public bool TryGetRandomPotato(out Potato potato);
        public bool TryAddPotato(Potato potato);
        public int PotatoCount { get; }
        public int MaxPotatoCount { get; }
    }
}