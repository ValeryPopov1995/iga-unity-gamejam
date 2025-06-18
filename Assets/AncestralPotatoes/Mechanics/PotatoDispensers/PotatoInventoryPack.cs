using AncestralPotatoes.Potatoes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AncestralPotatoes.PotatoDispancers
{
    [CreateAssetMenu(menuName = "Jam/PotatoInventoryPack")]
    public class PotatoInventoryPack : ScriptableObject
    {
        [Serializable]
        private struct PotatoCount
        {
            public Potato prefab;
            public int count;
        }

        [SerializeField] private PotatoCount[] potatoCounts;

        public List<Potato> GetCollection()
        {
            var potatos = new List<Potato>();
            for (var i = 0; i < potatoCounts.Length; i++)
                for (var j = 0; j < potatoCounts[i].count; j++)
                    potatos.Add(potatoCounts[i].prefab);
            return potatos;
        }
    }
}