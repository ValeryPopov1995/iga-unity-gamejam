using AncestralPotatoes.PotatoDispancers;
using AncestralPotatoes.Potatoes;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerHand : MonoBehaviour
    {
        public ReactiveProperty<Potato> SelectedPotato { get; private set; } = new();

        public ReactiveProperty<float> ThrowLoad01 => holdAction.ActionProgress;

        [SerializeField] private Vector2 throwMinMaxLoad01 = new(.3f, 1f);
        [SerializeField] private InputAction fire, change;
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private TrajectoryRenderer trajectoryRenderer;
        [SerializeField] private Transform hand;
        [SerializeField] private int forceCoef = 10;
        [SerializeField] private int randomTorque = 10;
        [SerializeField] private HoldAction holdAction;
        [Inject] private readonly DiContainer container;

        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
            inventory.OnPotatoAdded += TakeIfEmpty;
            Enable();
        }

        private void StartProgress(InputAction.CallbackContext context)
        {
            StartTrajectory();
            holdAction.Start();
        }

        private void EndProgress(InputAction.CallbackContext context)
        {
            trajectoryRenderer.DrawTrajectory(default, default);

            if (SelectedPotato.Value != null
            && ThrowLoad01.Value >= throwMinMaxLoad01.x
            && ThrowLoad01.Value <= throwMinMaxLoad01.y)
            {
                ThrowPotato(SelectedPotato.Value, ThrowLoad01.Value);
                SelectPotato();
                Debug.Log("throw potato", this);
            }

            holdAction.Cancel();
        }

        private Vector3 CalculateForce(float force01) => hand.forward * force01 * forceCoef;

        private void TakeIfEmpty(Potato newPotato)
        {
            if (SelectedPotato.Value == null)
                if (inventory.TryGetRandomPotato(out var potato))
                    SelectedPotato.Value = potato;
        }

        [Button]
        public void Enable()
        {
            fire.Enable();
            change.Enable();
            SelectPotato();
        }

        [Button]
        public void Disable()
        {
            fire.Disable();
            change.Disable();
        }

        private void OnDestroy()
        {
            Disable();
        }

        private async void StartTrajectory()
        {
            while (ThrowLoad01.Value > 0)
            {
                trajectoryRenderer.DrawTrajectory(hand.position, CalculateForce(ThrowLoad01.Value));
                await UniTask.NextFrame();
            }
        }

        private void ThrowPotato(Potato selected, float force01)
        {
            var potato = container.InstantiatePrefabForComponent<Potato>(selected, hand.position, hand.rotation, default);
            potato.GetRigidbody().AddForce(CalculateForce(ThrowLoad01.Value), ForceMode.Impulse);

            var torque = Random.insideUnitSphere * randomTorque;
            potato.GetRigidbody().AddTorque(torque, ForceMode.Impulse);

            selected = null;
        }

        private void SelectPotato()
        {
            if (!inventory.TryGetRandomPotato(out var potato))
                return;

            if (SelectedPotato.Value != null)
                inventory.TryAddPotato(SelectedPotato.Value);
            SelectedPotato.Value = potato;

        }
    }
}