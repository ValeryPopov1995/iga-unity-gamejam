using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace AncestralPotatoes.Potatoes
{
    [RequireComponent(typeof(Rigidbody))]
    public class Potato : MonoBehaviour, IPotato
    {
        private Rigidbody _rb;
        private IPotatoEffect[] _effects;
        public float DespawnTimeoutSeconds = 60;

        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Discription { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _effects = GetComponents<IPotatoEffect>();
            _ = DespawnAfterTimeoutAsync();
        }

        public Rigidbody GetRigidbody() => _rb;

        private void OnCollisionEnter(Collision collision) => HandleCollision(collision);

        protected void HandleCollision(Collision collision)
        {
            //Debug.Log("Collision");
            foreach (var effect in _effects)
                effect.Apply(collision);
        }

        private async UniTask DespawnAfterTimeoutAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DespawnTimeoutSeconds));
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Debug.Log("Removed potato");
        }
    }
}
