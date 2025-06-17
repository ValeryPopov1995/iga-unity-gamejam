using System;
using UnityEngine;

namespace AncestralPotatoes.PotatoDispancers
{
    public class Interaction : MonoBehaviour
    {
        public event Action<Interactable> OnEnter, OnExit;
        private Interactable interactable;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable))
            {
                Unselect();
                Select(interactable);
            }
        }

        private void Select(Interactable interactable)
        {
            this.interactable = interactable;
            interactable.Select(this);
            OnEnter?.Invoke(interactable);
        }

        private void Unselect()
        {
            if (interactable == null) return;
            interactable.Unselect();
            OnExit?.Invoke(interactable);
            interactable = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Interactable>(out var interactable) && this.interactable == interactable)
                Unselect();
        }
    }
}