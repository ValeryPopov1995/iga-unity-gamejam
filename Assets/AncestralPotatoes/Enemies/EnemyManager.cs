using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Enemies
{
    public class EnemyManager : MonoBehaviour, IEnemyLocator, IEnemySpawner
    {
        [SerializeField] private float dangerLevelIncreaseTimeout = 20f;
        [SerializeField] private int dangerLevel = 0;
        [SerializeField] private int fighters = 0;
        [Inject] private readonly IEnemyFactory _enemyFactory;
        [Inject] private readonly IEnemyBehaviourFactory _enemyBehaviourFactory;
        private readonly List<Enemy> fighterList = new();
        private readonly CancellationTokenSource cts = new();

        private void Start()
        {
            _ = StartDangerLevelUpdate();
        }

        private async UniTask StartDangerLevelUpdate()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(dangerLevelIncreaseTimeout));
                GlobalStats.IncrementDangerLevel();
                dangerLevel = GlobalStats.DangerLevel;
                Debug.Log($"Increased danger level: {GlobalStats.DangerLevel}");
            }
        }

        public Enemy GetClosestFighter(Vector3 position)
        {
            return fighterList.OrderBy(f => Vector3.Distance(position, f.transform.position)).FirstOrDefault();
        }

        public Enemy SpawnFighter(Vector3 position)
        {
            var enemy = _enemyFactory.CreateFighter(position);
            enemy.OnDeath += RemoveFighter;
            fighterList.Add(enemy);
            fighters++;
            var context = _enemyBehaviourFactory.CreateEnemyStateContext(enemy);
            enemy.StartBehaviour(context);
            GlobalStats.FightersOnLevel++;
            return enemy;
        }

        public Enemy SpawnSupport(Vector3 position)
        {
            var enemy = _enemyFactory.CreateSupport(position);
            var context = _enemyBehaviourFactory.CreateEnemyStateContext(enemy);
            enemy.StartBehaviour(context);
            GlobalStats.SupportsOnLevel++;
            return enemy;
        }

        private void RemoveFighter(Enemy fighter)
        {
            fighter.OnDeath -= RemoveFighter;
            fighterList.Remove(fighter);
            fighters--;
        }

        private void OnDestroy()
        {
            cts.Cancel();
        }
    }
}