using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemySpawnPoint : MonoBehaviour
    {
        [SerializeField] private EEnemyType enemyType;
        [Inject] private readonly EnemyManager enemyManager;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        [SerializeField] private Vector2 MinMaxSpawnDelay = new(5f, 20f);
        private float currentSpawnDelay;

        [SerializeField] private int maxEnemies = 3;
        private readonly List<Enemy> enemies = new();
        private void Awake()
        {
            currentSpawnDelay = MinMaxSpawnDelay.y;
            _ = StartSpawnLoop(cts.Token);
        }

        private float GetRandomDelay() => UnityEngine.Random.Range(MinMaxSpawnDelay.x, MinMaxSpawnDelay.y);

        private async UniTask StartSpawnLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(currentSpawnDelay));
                if (enemies.Count == maxEnemies)
                    continue;
                Enemy enemy = enemyType switch
                {
                    EEnemyType.Fighter => enemyManager.SpawnFighter(transform.position),
                    EEnemyType.Support => enemyManager.SpawnSupport(transform.position),
                    _ => null
                };
                if (enemy == null)
                    return;
                enemy.OnDeath += OnEnemyDeath;
                enemies.Add(enemy);

                currentSpawnDelay = GetRandomDelay();
            }
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            enemy.OnDeath -= OnEnemyDeath;
            enemies.Remove(enemy);
        }

        private void OnDestroy()
        {
            cts.Cancel();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = enemyType switch
            {
                EEnemyType.Fighter => new Color(1, 0, 0, 0.5f),
                EEnemyType.Support => new Color(0, 1, 0, 0.5f),
                _ => new Color(0, 1, 0, 0.5f),
            }
            ;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }
}