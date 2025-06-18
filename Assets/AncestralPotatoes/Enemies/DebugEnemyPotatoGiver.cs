using AncestralPotatoes.Enemies;
using AncestralPotatoes.Potatoes;
using NaughtyAttributes;
using UnityEngine;

public class DebugEnemyPotatoGiver : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Potato potatoPrefab;
    [Button]
    public void GivePotato()
    {
        enemy.PotatoInventory.TryAddPotato(potatoPrefab);
    }
}