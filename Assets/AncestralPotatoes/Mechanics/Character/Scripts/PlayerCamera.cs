using Unity.Cinemachine;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineImpulseSource impulseSsource;

        public void Shake(float force)
        {
            impulseSsource.GenerateImpulse(force);
        }
    }
}