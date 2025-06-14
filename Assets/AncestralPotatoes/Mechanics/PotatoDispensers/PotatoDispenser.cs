using AncestralPotatoes.Character;
using AncestralPotatoes.Potatoes;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.PotatoDispancers
{
    public class PotatoDispenser : MonoBehaviour
    {
        [SerializeField] private Potato potatoPrefab;
        [Inject] private readonly Player player;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != player.gameObject) return;
            player.Inventory.TryAddPotato(potatoPrefab);
#if UNITY_EDITOR
            Debug.Log("potato dispensed", this);
#endif
        }
    }
}