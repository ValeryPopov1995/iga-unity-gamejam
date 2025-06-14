using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Enemies.Debug
{
    public class DebugEnemyController : MonoBehaviour
    {
        [Inject] private Camera _camera;
        [SerializeField] private InputAction _cursorClick;
        [SerializeField] private InputAction _cursorMove;
        [SerializeField] private Enemy _enemy;

        private Vector2 _cursorPos;
        
        private void Awake()
        {
            _cursorMove.performed += OnCursorMoved;
            _cursorClick.performed += OnCursorClicked;
        }

        private void OnCursorClicked(InputAction.CallbackContext context)
        {
            var ray = _camera.ScreenPointToRay(_cursorPos);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _enemy.SetTargetPosition(hitInfo.point);
            }
        }

        private void OnCursorMoved(InputAction.CallbackContext context)
        {
            _cursorPos = context.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            _cursorMove.Enable();
            _cursorClick.Enable();
        }

        private void OnDisable()
        {
            _cursorMove.Disable();
            _cursorClick.Disable();
        }
        
    }
}