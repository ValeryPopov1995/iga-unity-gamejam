using UnityEngine;

namespace AncestralPotatoes.Mechanics.PotatoDispensers
{
    public interface IPotatoDispenserLocator
    {
        public void Register(IPotatoDispenser dispenser);
        public IPotatoDispenser GetClosestDispenserWithPotato(Vector3 position);
    }
}
