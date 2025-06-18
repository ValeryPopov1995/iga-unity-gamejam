using UnityEngine;

namespace AncestralPotatoes
{
    public class DamageDescription
    {
        public float Amount { get; set; }
        public Vector3 Point { get; set; }
        public Vector3 Force { get; set; }
        public EDamageType Type { get; set; }
    }
}