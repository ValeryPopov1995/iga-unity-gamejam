using UnityEngine;

namespace AncestralPotatoes.Helicopters
{
    public class Helicopter : MonoBehaviour
    {
        [SerializeField] private Transform lookTarget;

        private void LateUpdate()
        {
            transform.LookAt(lookTarget);
        }
    }
}