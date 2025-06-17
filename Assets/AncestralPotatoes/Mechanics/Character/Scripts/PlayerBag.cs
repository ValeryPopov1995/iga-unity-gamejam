using AncestralPotatoes.Potatoes;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerBag : MonoBehaviour
    {
        [SerializeField] private Vector2 scaleRange = new(.2f, 1);
        [Inject] private readonly Player player;

        private void Start()
        {
            player.Inventory.OnPotatoAdded += UpdateScale;
            player.Inventory.OnPotatoRemoved += UpdateScale;
            UpdateScale();
        }

        private void OnDestroy()
        {
            player.Inventory.OnPotatoAdded -= UpdateScale;
            player.Inventory.OnPotatoRemoved -= UpdateScale;
        }

        private void UpdateScale(Potato potato) => UpdateScale();

        private void UpdateScale()
        {
            var laod01 = (float)player.Inventory.PotatoCount / player.Inventory.MaxPotatoCount;
            var scale = scaleRange.x + laod01 * (scaleRange.y - scaleRange.x);
            transform.localScale = Vector3.one * scale;
        }
    }
}