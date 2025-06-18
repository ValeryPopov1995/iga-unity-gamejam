using AncestralPotatoes.Character;
using AncestralPotatoes.PotatoDispancers;
using AncestralPotatoes.Scene;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Cart
{
    public class CartInventory : Interactable
    {
        [field: SerializeField] public PotatoInventory Inventory { get; private set; }

        [SerializeField] private Transform[] bags;
        [SerializeField] private float getForce, getTorque;
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private AudioClip getClip, giveClip;
        [Inject] private readonly Player player;
        [Inject] private readonly SfxPlayer sfxPlayer;

        protected override void Start()
        {
            base.Start();
            UpdateBags();
        }

        protected override void Interact(float progress01)
        {
            base.Interact(progress01);
            if (progress01 <= .5f)
                GetPotato();
            else if (progress01 == 1)
                GivePotato();

            UpdateBags();
            Force();
        }

        private void GetPotato()
        {
            if (Inventory.PotatoCount < Inventory.MaxPotatoCount
                && player.Hand.SelectedPotato.Value != null)
            {
                _ = Inventory.TryAddPotato(player.Hand.SelectedPotato.Value);
                player.Hand.SelectedPotato.Value = null;
                sfxPlayer.PlayOneShot(getClip, transform.position);
            }
        }

        private void GivePotato()
        {
            if (Inventory.PotatoCount > 0
                && player.Inventory.PotatoCount < player.Inventory.MaxPotatoCount
                && Inventory.TryGetRandomPotato(out var potato))
            {
                player.Inventory.TryAddPotato(potato);
                sfxPlayer.PlayOneShot(giveClip, transform.position);
            }
        }

        private void UpdateBags()
        {
            var count = (float)Inventory.PotatoCount / Inventory.MaxPotatoCount * bags.Length;
            for (var i = 0; i < bags.Length; i++)
            {
                if (i < count)
                    bags[i].gameObject.SetActive(true);
                else
                    bags[i].gameObject.SetActive(false);
            }
        }

        private void Force()
        {
            rigidbody.AddForce(Random.insideUnitSphere * getForce);
            rigidbody.AddTorque(Random.insideUnitSphere * getForce);
        }
    }
}