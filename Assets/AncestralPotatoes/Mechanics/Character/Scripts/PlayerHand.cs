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
        public ReactiveProperty<Potato> SelectedPrefab { get; private set; } = new();

        public ReactiveProperty<float> ThrowLoad01 = new();

        [SerializeField] private Vector2 throwMinMaxLoad01 = new(.3f, 1f);
        [SerializeField] private float throwDuration = 2;
        [SerializeField] private InputAction fire, change;
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private TrajectoryRenderer trajectoryRenderer;
        [SerializeField] private Transform hand;
        [SerializeField] private int forceCoef = 10;
        [SerializeField] private int randomTorque = 10;
        [Inject] private readonly DiContainer container;
        private CancellationTokenSource source;

        private void Start()
        {
            fire.started += StartProgress;
            fire.canceled += EndProgress;
            inventory.OnAdd += TrySelect;

            Enable();
        }
        private Vector3 CalculateForce(float force01) => hand.forward * force01 * forceCoef;

        private void TrySelect(Potato potato)
        {
            if (SelectedPrefab.Value == null)
                SelectedPrefab.Value = potato;
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
            StartTrajectory(token);
            while (ThrowLoad01.Value < 1 && !token.IsCancellationRequested)
            {
                ThrowLoad01.Value += Time.deltaTime / throwDuration;
                await UniTask.NextFrame();
            }

            if (!token.IsCancellationRequested)
                ThrowLoad01.Value = 1;
        }

        private async void StartTrajectory(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                trajectoryRenderer.DrawTrajectory(hand.position, CalculateForce(ThrowLoad01.Value));
                await UniTask.NextFrame();
            }
        }

        private void EndProgress(InputAction.CallbackContext context)
        {
            if (SelectedPrefab.Value != null
                && ThrowLoad01.Value >= throwMinMaxLoad01.x
                && ThrowLoad01.Value <= throwMinMaxLoad01.y)
            {
                ThrowPotato(SelectedPrefab.Value, ThrowLoad01.Value);
#if UNITY_EDITOR
                Debug.Log("throw potato", this);
#endif
            }

            source?.Cancel();
            ThrowLoad01.Value = 0;
            trajectoryRenderer.DrawTrajectory(default, default);
        }

        private void ThrowPotato(Potato selected, float force01)
        {
            var potato = container.InstantiatePrefabForComponent<Potato>(selected, hand.position, hand.rotation, default);
            potato.GetRigidbody().AddForce(CalculateForce(ThrowLoad01.Value), ForceMode.Impulse);

            var torque = Random.insideUnitSphere * randomTorque;
            potato.GetRigidbody().AddTorque(torque, ForceMode.Impulse);
        }

        private void SelectPotato()
        {
            var next = inventory.RandomNext;
            inventory.TryRemovePotato(next);
            if (SelectedPrefab.Value != null)
                inventory.TryAddPotato(SelectedPrefab.Value);
            SelectedPrefab.Value = next;
        }
    }
}