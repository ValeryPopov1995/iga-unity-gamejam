using UnityEngine;

namespace AncestralPotatoes.Potatoes
{
    public interface IPotato
    {
        string Name { get; }
        string Discription { get; }
        Rigidbody GetRigidbody();
        GameObject CreateVisualInstance();
    }
}