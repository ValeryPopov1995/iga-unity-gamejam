using AncestralPotatoes.Cart;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class Player : MonoBehaviour, IDamageReceiver, IPlayerModificator
    {
        public event Action OnDeath;

        [field: SerializeField] public float Health { get; private set; } = 30f;
        public PotatoInventory Inventory { get; private set; }
        public PlayerHand Hand { get; private set; }
        public PlayerAnimator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public IRagdoll Ragdoll { get; private set; }

        public float MoveCoef { get; private set; }
        public float JumpCoef { get; private set; }
        public float RotateCoef { get; private set; }
        public bool IsDead => Health <= 0;


        [SerializeField] private float demageShake = 1;
        [Inject] private readonly PlayerCamera playerCamera;
        private readonly HashSet<IPlayerModificator> modificators = new();

        private void Awake()
        {
            Inventory = GetComponentInChildren<PotatoInventory>();
            Hand = GetComponentInChildren<PlayerHand>();
            Rigidbody = GetComponentInChildren<Rigidbody>();
            Animator = GetComponentInChildren<PlayerAnimator>();
            Ragdoll = GetComponentInChildren<PlayerRagdoll>();
            CalculateModificators();
        }

        public void ReceiveDamage(DamageDescription damage)
        {
            if (IsDead) return;

            Health -= damage.Amount;
            playerCamera.Shake(demageShake);
            Debug.Log($"Received {damage.Amount} damage ({Health})");

            if (IsDead)
            {
                Ragdoll.SetRagdoll(true);
                OnDeath?.Invoke();
            }
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

        public Vector3 GetPosition() => transform.position;
    }
}