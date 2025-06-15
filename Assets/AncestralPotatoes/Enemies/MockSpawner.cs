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
            //click.performed += OnClick;
            move.performed += OnMove;
        }

        private void OnDestroy()
        {
            //click.performed -= OnClick;
            move.performed -= OnMove;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }

        //private void OnClick(InputAction.CallbackContext context)
        //{
        //    var camera = Camera.main;
        //    var ray = camera.ScreenPointToRay(_mousePos);
        //    if (Physics.Raycast(ray, out var hitInfo))
        //    {
        //        transform.position = hitInfo.point;
        //        Spawn(transform.position);
        //    }
        //}

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
        public void Spawn()
        {
            _enemyManager.CreateEnemy(transform.position);
        }
    }
}