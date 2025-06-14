using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu]
    public class PotatoDiscriptionMockup : ScriptableObject
    {
        [field: SerializeField] public PotatoMockup Prefab { get; internal set; }
    }
}