using AncestralPotatoes.Potatoes;
using System;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Mechanics.PotatoDispensers
{
    public class PotatoDispenserMock : MonoBehaviour, IPotatoDispenser
    {
        [Inject] private readonly IPotatoDispenserLocator locator;

        [SerializeField] private Potato potatoPrefab;

        public event Action<IPotatoDispenser> OnDispenserDestroy;

        private void Awake()
        {
            locator.Register(this);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool TryGetPotato(out Potato potato)
        {
            potato = potatoPrefab;
            return true;
        }

        private void OnDestroy()
        {
            OnDispenserDestroy?.Invoke(this);
        }
    }
}
