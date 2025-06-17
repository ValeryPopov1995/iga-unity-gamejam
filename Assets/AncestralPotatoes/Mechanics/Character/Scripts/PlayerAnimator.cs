using NaughtyAttributes;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField, AnimatorParam(nameof(animator))] private string strike, fire, move;

        public void Strike() => animator.SetTrigger(strike);
        public void Fire() => animator.SetTrigger(fire);
        public void Move(bool move) => animator.SetBool(this.move, move);
    }
}