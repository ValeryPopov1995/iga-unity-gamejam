using AncestralPotatoes.Potatoes;
using UniRx;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class ThrowingHand : MonoBehaviour
    {
        public ReactiveProperty<Potato> SelectedPotato { get; private set; } = new();

        [SerializeField] protected PotatoInventory inventory;
        [SerializeField] protected Transform spawnPoint;
        [SerializeField] protected int forceCoef = 10;
        [SerializeField] protected int randomTorque = 10;
        [Inject] protected readonly DiContainer container;

        protected Vector3 CalculateForce(float force01) => spawnPoint.forward * force01 * forceCoef;

        public virtual void ThrowPotato(float force01)
        {
            var potato = container.InstantiatePrefabForComponent<Potato>(SelectedPotato.Value, spawnPoint.position, spawnPoint.rotation, default);
            potato.transform.SetParent(null); // i dont know why
            potato.GetRigidbody().AddForce(CalculateForce(force01), ForceMode.Impulse);

            var torque = Random.insideUnitSphere * randomTorque;
            potato.GetRigidbody().AddTorque(torque, ForceMode.Impulse);

            SelectedPotato.Value = null;
        }

        public virtual void SelectPotato()
        {
            if (!inventory.TryGetRandomPotato(out var potato)) return;

            if (SelectedPotato.Value != null)
                inventory.TryAddPotato(SelectedPotato.Value);
            SelectedPotato.Value = potato;
        }
    }
}