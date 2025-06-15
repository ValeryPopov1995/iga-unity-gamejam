using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncestralPotatoes.PotatoDispancers
{
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string ActionDesription { get; private set; }
        [field: SerializeField] public HoldAction InteractAction { get; private set; }
        [SerializeField] private InputAction input;

        private void Start()
        {
            input.started += ctx => InteractAction.Start();
            InteractAction.ActionProgress.Subscribe(TryInteract);
            input.canceled += ctx => InteractAction.Cancel();
        }

        private void TryInteract(float progress)
        {
            if (InteractAction.ActionProgress.Value < 1) return;
            Intercat();
            InteractAction.Cancel();
        }

        protected virtual void Intercat() { }
    }
}