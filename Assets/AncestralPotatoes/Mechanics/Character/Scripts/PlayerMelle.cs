using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerMelle : MonoBehaviour
    {
        [SerializeField] private InputAction strike;
        [SerializeField] private float cooldown = .9f;
        private float time;
        [Inject] private readonly PlayerAnimator playerAnimator;

        private void Start()
        {
            strike.started += Strike;
            strike.Enable();
        }

        private void OnDestroy()
        {
            strike.started -= Strike;
            strike.Disable();
        }

        private void Strike(InputAction.CallbackContext context)
        {
            if (time + cooldown > Time.time) return;
            playerAnimator.Strike();
            time = Time.time;
        }
    }
}