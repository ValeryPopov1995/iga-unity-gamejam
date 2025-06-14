using AncestralPotatoes.Potatoes;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Character
{

    public class PlayerHand : MonoBehaviour
    {
        public Potato SelectedPrefab { get; private set; }

        public ReactiveProperty<float> ThrowLoad01 = new();

        [SerializeField] private Vector2 throwMinMaxLoad01 = new(.3f, 1f);
        [SerializeField] private float throwDuration = 2;
        [SerializeField] private InputAction fire, change;
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private Transform hand;
        [SerializeField] private float forceCoef = 10;
        [Inject] private readonly DiContainer container;
        private CancellationTokenSource source;


        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
            Enable();
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

        private async void StartProgress(InputAction.CallbackContext context)
        {
            source?.Cancel();
            source = new();
            var token = source.Token;
            while (ThrowLoad01.Value < 1 && !token.IsCancellationRequested)
            {
                ThrowLoad01.Value += Time.deltaTime / throwDuration;
                await UniTask.NextFrame();
            }

            if (!token.IsCancellationRequested)
                ThrowLoad01.Value = 1;
        }

        private void EndProgress(InputAction.CallbackContext context)
        {
            if (SelectedPrefab != null
                && ThrowLoad01.Value >= throwMinMaxLoad01.x
                && ThrowLoad01.Value <= throwMinMaxLoad01.y)
                ThrowPotato(SelectedPrefab, ThrowLoad01.Value);

            source?.Cancel();
            ThrowLoad01.Value = 0;
        }

        private void ThrowPotato(Potato selected, float force01)
        {
            var potato = container.InstantiatePrefabForComponent<Potato>(selected, hand.position, hand.rotation, default);
            potato.GetRigidbody().AddForce(hand.forward * force01 * forceCoef, ForceMode.Impulse);
        }

        private void SelectPotato()
        {
            var next = inventory.RandomNext;
            inventory.TryRemovePotato(next);
            if (SelectedPrefab != null)
                inventory.TryAddPotato(SelectedPrefab);
            SelectedPrefab = next;
        }
    }
}