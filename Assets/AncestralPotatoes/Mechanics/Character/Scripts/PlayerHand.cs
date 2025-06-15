using AncestralPotatoes.PotatoDispancers;
using AncestralPotatoes.Potatoes;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncestralPotatoes.Character
{
    public class PlayerHand : ThrowingHand
    {
        public ReactiveProperty<float> ThrowLoad01 => holdAction.ActionProgress;

        [SerializeField] private Vector2 throwMinMaxLoad01 = new(.3f, 1f);
        [SerializeField] private InputAction fire, change;
        [SerializeField] private TrajectoryRenderer trajectoryRenderer;
        [SerializeField] private HoldAction holdAction;

        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
            change.started += SelectPotato;
            inventory.OnPotatoAdded += TakeIfEmpty;
            Enable();
        }

        private void SelectPotato(InputAction.CallbackContext context)
        {
            SelectPotato();
        }

        private void StartProgress(InputAction.CallbackContext context)
        {
            if (SelectedPotato.Value == null) return;
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
                ThrowPotato(ThrowLoad01.Value);
                SelectPotato();
                Debug.Log("throw potato", this);
            }

            holdAction.Cancel();
        }

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
            fire.started -= StartProgress;
            fire.canceled -= EndProgress;
            change.started -= SelectPotato;
            inventory.OnPotatoAdded -= TakeIfEmpty;
            Disable();
        }

        private async void StartTrajectory()
        {
            await UniTask.NextFrame();
            while (ThrowLoad01.Value > 0)
            {
                trajectoryRenderer.DrawTrajectory(spawnPoint.position, CalculateForce(ThrowLoad01.Value));
                await UniTask.NextFrame();
            }
        }
    }
}