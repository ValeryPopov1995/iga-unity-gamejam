using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Character
{
    public class PlayerHand : MonoBehaviour
    {
        public ReactiveProperty<float> ForcePorgress = new();
        [SerializeField] private InputAction fire, change;
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private Transform hand;
        [SerializeField] private float forceCoef = 10;
        [Inject] private readonly DiContainer container;
        private CancellationTokenSource source;

        public PotatoDiscriptionMockup Selected;// { get; private set; }

        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
        }

        private async void StartProgress(InputAction.CallbackContext context)
        {
            source?.Cancel();
            source = new();
            var token = source.Token;
            while (ForcePorgress.Value < 1 && !token.IsCancellationRequested)
            {
                ForcePorgress.Value += Time.deltaTime;
                await UniTask.NextFrame();
            }

            if (!token.IsCancellationRequested)
                ForcePorgress.Value = 1;
        }

        private void EndProgress(InputAction.CallbackContext context)
        {
            if (ForcePorgress.Value == 1)
                ThrowPotato(Selected, ForcePorgress.Value);

            source?.Cancel();
            ForcePorgress.Value = 0;
        }

        private void ThrowPotato(PotatoDiscriptionMockup selected, float force01)
        {
            var potato = container.InstantiatePrefabForComponent<PotatoMockup>(selected.Prefab, hand.position, hand.rotation, default);
            potato.Rigidbody.AddForce(hand.forward * force01 * forceCoef, ForceMode.Impulse);
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

        private void SelectPotato()
        {
            var next = inventory.RandomNext;
            inventory.TryRemovePotato(next);
            if (Selected != null)
                inventory.TryAddPotato(Selected);
            Selected = next;
        }
    }
}