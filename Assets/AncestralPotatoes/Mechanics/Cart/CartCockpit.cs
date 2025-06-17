using AncestralPotatoes.Character;
using AncestralPotatoes.PotatoDispancers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Cart
{
    public class CartCockpit : Interactable, IPlayerModificator
    {
        [SerializeField] private SpringJoint joint;
        [SerializeField] private InputAction unlinkAction;
        [SerializeField] private readonly float spring = 300;
        [Inject] private readonly Player player;

        public float MoveCoef => .5f;
        public float JumpCoef => .5f;
        public float RotateCoef => .5f;

        protected override void Start()
        {
            base.Start();
            unlinkAction.started += ctx => Unlink();
            unlinkAction.Enable();
            Unlink();
        }

        protected override void Interact(float progress01)
        {
            base.Interact(progress01);
            if (progress01 < 1)
                Unlink();
            else Link();
        }

        private void Link()
        {
            joint.connectedBody = player.Rigidbody;
            joint.spring = spring;
            player.AddModificator(this);
        }

        private void Unlink()
        {
            joint.connectedBody = default;
            joint.spring = default;
            player.RemoveModificator(this);
        }
    }
}