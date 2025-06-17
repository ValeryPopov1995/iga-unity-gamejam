using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AncestralPotatoes.Mechanics.PotatoDispensers
{
    public class PotatoDispenserLocatorMock : MonoBehaviour, IPotatoDispenserLocator
    {
        private readonly List<IPotatoDispenser> dispensers = new();

        public void Register(IPotatoDispenser dispenser)
        {
            dispensers.Add(dispenser);
            dispenser.OnDispenserDestroy += Dispenser_OnDispenserDestroy;
        }

        private void Dispenser_OnDispenserDestroy(IPotatoDispenser dispenser)
        {
            dispensers.Remove(dispenser);
            dispenser.OnDispenserDestroy -= Dispenser_OnDispenserDestroy;
        }

        public IPotatoDispenser GetClosestDispenserWithPotato(Vector3 position)
        {
            return dispensers.OrderBy(d => Vector3.Distance(position, d.GetPosition())).FirstOrDefault();
        }
    }
}
