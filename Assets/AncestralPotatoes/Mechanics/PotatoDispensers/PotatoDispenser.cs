using AncestralPotatoes.Character;
using AncestralPotatoes.Mechanics.PotatoDispensers;
using AncestralPotatoes.Potatoes;
using System;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.PotatoDispancers
{
    public class PotatoDispenser : Interactable, IPotatoDispenser
    {
        [SerializeField] private PotatoInventoryPack inventoryPack;
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private Vector2Int takeCount = new(2, 4);
        [Inject] private readonly Player player;
        [Inject] private readonly IPotatoDispenserLocator locator;

        public event Action<IPotatoDispenser> OnDispenserDestroy;

        protected override void Awake()
        {
            base.Awake();
            locator.Register(this);
            inventory.AddPotatos(inventoryPack.GetCollection());
        }

        protected override void Interact(float progress01)
        {
            base.Interact(progress01);
            if (progress01 < 1)
                return;

            if (inventory.TryGetRandomPotato(out var potato))
            {
                var count = UnityEngine.Random.Range(takeCount.x, takeCount.y + 1);
                for (var i = 0; i < count; i++)
                    player.Inventory.TryAddPotato(potato);
                Debug.Log("potato dispensed", this);
            }
        }

        public Vector3 GetPosition() => transform.position;

        public bool TryGetPotato(out Potato potato) => inventory.TryGetRandomPotato(out potato);

        public int GetPotatoCount()
        {
            return inventory.PotatoCount;
        }
    }
}