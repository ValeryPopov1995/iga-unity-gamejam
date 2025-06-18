using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class DeathCamera : MonoBehaviour
    {
        [Inject] private readonly Player player;

        private void Start()
        {
            player.OnDeath += () => gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}