using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AncestralPotatoes.Character
{
    public class PlayerAnimator : MonoBehaviour
    {
        public void Strike() => animator.SetTrigger(strike);
        public void Fire() => animator.SetTrigger(fire);
        public void Move(bool move) => animator.SetBool(this.move, move);

        [SerializeField] private Animator animator;
        [SerializeField] private InputAction moveAction;
        [SerializeField, AnimatorParam(nameof(animator))] private string strike, fire, move;
        [SerializeField] private int updateDelay = 500;
        private bool moved;

        private async void OnEnable()
        {
            moveAction.Enable();

            while (this != null)
            {
                var moved = moveAction.ReadValue<Vector2>().magnitude > 0;
                if (this.moved != moved)
                {
                    this.moved = moved;
                    Move(this.moved);
                }
                await UniTask.Delay(updateDelay);
            }
        }

        private void OnDisable()
        {
            moveAction.Disable();
        }
    }
}