using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class MockSpawner : MonoBehaviour
    {
        [SerializeField] private InputAction click;
        [SerializeField] private InputAction move;

        [Inject] private readonly EnemyManager _enemyManager;

        private Vector2 _mousePos;

        private void Awake()
        {
            move.performed += OnMove;
        }

        private void OnDestroy()
        {
            move.performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }


        private void OnEnable()
        {
            click.Enable();
            move.Enable();
        }

        private void OnDisable()
        {
            click.Disable();
            move.Disable();
        }

        [Button]
        public void SpawnFighter()
        {
            _enemyManager.SpawnFighter(transform.position);
        }

        [Button]
        public void SpawnSupport()
        {
            _enemyManager.SpawnSupport(transform.position);
        }
    }
}