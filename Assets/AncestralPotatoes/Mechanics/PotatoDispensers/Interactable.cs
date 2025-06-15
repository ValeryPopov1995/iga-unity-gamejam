using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncestralPotatoes.PotatoDispancers
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string ActionDesription { get; private set; }
        [field: SerializeField] public HoldAction InteractAction { get; private set; }

        protected Interaction interaction;

        [SerializeField] private InputAction input;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void Start()
        {
            input.started += ctx => InteractAction.Start();
            InteractAction.ActionProgress.Subscribe(TryInteractOnFull);
            input.canceled += ctx =>
            {
                if (InteractAction.ActionProgress.Value < 1)
                    Interact(InteractAction.ActionProgress.Value);
                InteractAction.Cancel();
            };
        }

        public void Select(Interaction interaction)
        {
            this.interaction = interaction;
            input.Enable();
        }

        public void Unselect()
        {
            interaction = null;
            input.Disable();
        }

        protected virtual void TryInteractOnFull(float progress01)
        {
            if (progress01 < 1) return;
            Interact(progress01);
        }

        protected virtual void Interact(float progress01) { }
    }
}