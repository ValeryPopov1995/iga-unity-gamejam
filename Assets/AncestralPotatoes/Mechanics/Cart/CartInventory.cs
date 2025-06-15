using AncestralPotatoes.Character;
using AncestralPotatoes.PotatoDispancers;
using System;
using Zenject;

namespace AncestralPotatoes.Cart
{
    public class CartInventory : Interactable
    {
        public PotatoInventory Inventory { get; private set; } = new();

        [Inject] private readonly Player player;

        protected override void Interact(float progress01)
        {
            base.Interact(progress01);
            if (progress01 <= .1f)
                GetPotato();
            else
                GivePotato();
        }

        private void GetPotato()
        {

            player.Hand.SelectedPotato.Value = null;
        }

        private void GivePotato()
        {
            throw new NotImplementedException();
        }
    }
}