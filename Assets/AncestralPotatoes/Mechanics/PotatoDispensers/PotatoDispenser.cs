using AncestralPotatoes.Character;
using AncestralPotatoes.Potatoes;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.PotatoDispancers
{
    public class PotatoDispenser : Interactable
    {
        [SerializeField] private Potato potatoPrefab;
        [Inject] private readonly Player player;

        protected override void Intercat()
        {
            base.Intercat();
            player.Inventory.TryAddPotato(potatoPrefab);
            Debug.Log("potato dispensed", this);
        }
    }
}