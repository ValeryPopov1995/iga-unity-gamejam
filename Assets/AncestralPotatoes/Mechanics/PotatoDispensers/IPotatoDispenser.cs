using AncestralPotatoes.Potatoes;
using System;
using UnityEngine;

namespace AncestralPotatoes.Mechanics.PotatoDispensers
{
    public interface IPotatoDispenser
    {
        public Vector3 GetPosition();
        public bool TryGetPotato(out Potato potato);

        public event Action<IPotatoDispenser> OnDispenserDestroy;
    }
}
