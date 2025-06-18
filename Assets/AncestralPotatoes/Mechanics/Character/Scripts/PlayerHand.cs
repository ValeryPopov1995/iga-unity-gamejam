using AncestralPotatoes.PotatoDispancers;
using AncestralPotatoes.Potatoes;
using AncestralPotatoes.Scene;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerHand : ThrowingHand
    {
        public ReactiveProperty<float> ThrowLoad01 => holdAction.ActionProgress;

        [SerializeField] private Vector2 throwMinMaxLoad01 = new(.3f, 1f);
        [SerializeField] private InputAction fire, change;
        [SerializeField] private TrajectoryRenderer trajectoryRenderer;
        [SerializeField] private HoldAction holdAction;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private AudioClip switchClip;
        [SerializeField] private Transform visualParent;
        [Inject] private readonly Player player;
        [Inject] private readonly SfxPlayer sfxPlayer;
        private GameObject visual;

        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
            change.started += SelectPotato;

            inventory.OnPotatoAdded += TakeIfEmpty;
            SelectedPotato.Subscribe(TakeIfEmpty).AddTo(this);
            SelectedPotato.Subscribe(UpdateVisual).AddTo(this);

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

        private async void UpdateVisual(Potato potato)
        {
            if (visual != null)
            {
                Destroy(visual);
                await UniTask.NextFrame();
            }

            if (potato != null)
            {
                var instance = potato.CreateVisualInstance();
                instance.transform.SetParent(visualParent, false);
                instance.transform.SetLocalPositionAndRotation(default, default);
                visual = instance;
            }
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

        public override void ThrowPotato(float force01)
        {
            base.ThrowPotato(force01);
            player.Animator.Fire();
            sfxPlayer.PlayOneShot(fireClip, transform.position);
        }

        public override void SelectPotato()
        {
            base.SelectPotato();
            sfxPlayer.PlayOneShot(switchClip, transform.position);
        }
    }
}