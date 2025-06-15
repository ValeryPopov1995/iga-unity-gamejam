using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class MockSpawner : MonoBehaviour
    {
        [SerializeField] private InputAction _click;
        [SerializeField] private InputAction _move;

        [Inject] private readonly EnemyManager _enemyManager;

        private Vector2 _mousePos;
        private Vector3 _spawnerPos;

        private void Awake()
        {
            _click.performed += OnClick;
            _move.performed += OnMove;
        }

        private void OnDestroy()
        {
            _click.performed -= OnClick;
            _move.performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            var camera = Camera.main;
            var ray = camera.ScreenPointToRay(_mousePos);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _spawnerPos = hitInfo.point;
            }
        }

        private void OnEnable()
        {
            _click.Enable();
            _move.Enable();
        }

        private void OnDisable()
        {
            _click.Disable();
            _move.Disable();
        }

        [Button]
        public void Spawn()
        {
            _enemyManager.CreateEnemy(_spawnerPos);
        }
    }
}