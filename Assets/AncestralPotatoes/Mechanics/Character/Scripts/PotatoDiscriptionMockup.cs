using UnityEngine;

namespace Game.Character
{
    [CreateAssetMenu]
    public class PotatoDiscriptionMockup : ScriptableObject
    {
        [field: SerializeField] public string Name;
        [field: SerializeField] public string Discription;
        [field: SerializeField] public PotatoMockup Prefab { get; internal set; }
    }
}