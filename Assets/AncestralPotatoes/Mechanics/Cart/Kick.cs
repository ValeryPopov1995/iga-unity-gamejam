using AncestralPotatoes.PotatoDispancers;
using UnityEngine;

namespace AncestralPotatoes.Cart
{
    public class Kick : Interactable
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Vector3 force;
        [SerializeField] private Vector3 torque;

        protected override void Interact(float progress01)
        {
            base.Interact(progress01);
            rigidbody.AddRelativeForce(force);
            rigidbody.AddRelativeTorque(torque);
        }
    }
}