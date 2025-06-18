using TMPro;
using UnityEngine;

namespace AncestralPotatoes.Character
{
    public class InventoryCountText : MonoBehaviour
    {
        [SerializeField] private PotatoInventory inventory;
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            inventory.OnPotatoAdded += potato => text.text = inventory.PotatoCount.ToString();
            inventory.OnPotatoRemoved += potato => text.text = inventory.PotatoCount.ToString();
        }
    }
}