using Cysharp.Threading.Tasks;
using KinematicCharacterController;
using NaughtyAttributes;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class PlayerRagdoll : MonoBehaviour, IRagdoll
    {
        [SerializeField] private KinematicCharacterMotor motor;
        [SerializeField] private Rigidbody rigidbody;

        public bool IsRagdoll => rigidbody.isKinematic;

#if UNITY_EDITOR
        [Button] private void EnableRagdoll() => _ = SetRagdoll(true);
        [Button] private void DisableRagdoll() => _ = SetRagdoll(false);
#endif

        public async UniTask SetRagdoll(bool state)
        {
            if (!state)
                motor.SetPosition(motor.transform.position);
            motor.enabled = !state;
            rigidbody.isKinematic = !state;
            await UniTask.NextFrame();
        }
    }
}