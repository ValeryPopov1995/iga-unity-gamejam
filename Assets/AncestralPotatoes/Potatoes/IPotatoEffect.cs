using UnityEngine;

namespace AncestralPotatoes.Potatoes
{
    public interface IPotatoEffect
    {
        void Apply(Collision collision);
    }
}