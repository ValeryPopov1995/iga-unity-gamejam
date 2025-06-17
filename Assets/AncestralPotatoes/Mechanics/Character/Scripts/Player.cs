using AncestralPotatoes.Cart;
using System.Collections.Generic;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class Player : MonoBehaviour, IDamageReceiver, IPlayerModificator
    {
        public float Health { get; private set; }
        public PotatoInventory Inventory { get; private set; }
        public PlayerHand Hand { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        public float MoveCoef { get; private set; }
        public float JumpCoef { get; private set; }
        public float RotateCoef { get; private set; }

        private readonly HashSet<IPlayerModificator> modificators = new();

        public void ReceiveDamage(DamageDescription damage)
        {
            Health -= damage.Amount;
        }

        internal void AddModificator(IPlayerModificator cartCockpit)
        {
            modificators.Add(cartCockpit);
            CalculateModificators();
        }

        internal void RemoveModificator(IPlayerModificator cartCockpit)
        {
            modificators.Remove(cartCockpit);
            CalculateModificators();
        }

        private void Awake()
        {
            Inventory = GetComponentInChildren<PotatoInventory>();
            Hand = GetComponentInChildren<PlayerHand>();
            Rigidbody = GetComponentInChildren<Rigidbody>();
            CalculateModificators();
        }

        private void CalculateModificators()
        {
            float move = 1, jump = 1, rotate = 1;

            foreach (var mod in modificators)
            {
                move *= mod.MoveCoef;
                jump *= mod.JumpCoef;
                rotate *= mod.RotateCoef;
            }

            MoveCoef = move;
            JumpCoef = jump;
            RotateCoef = rotate;
        }
    }
}