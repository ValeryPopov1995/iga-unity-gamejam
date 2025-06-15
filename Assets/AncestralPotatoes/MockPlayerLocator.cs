using UnityEngine;

namespace AncestralPotatoes
{
    internal class MockPlayerLocator : MonoBehaviour, IPlayerLocator
    {
        public Vector3 GetPlayerPosition()
        {
            return transform.position;
        }
    }
}