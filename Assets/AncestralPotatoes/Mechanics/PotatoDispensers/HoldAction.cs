using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

namespace AncestralPotatoes.PotatoDispancers
{
    [Serializable]
    public class HoldAction
    {
        [field: SerializeField] public float ActionDuration { get; private set; } = 1;
        public ReactiveProperty<float> ActionProgress = new();
        private CancellationTokenSource source;

        public async void Start()
        {
            source?.Cancel();
            source = new();
            var token = source.Token;
            while (ActionProgress.Value < 1 && !token.IsCancellationRequested)
            {
                ActionProgress.Value = Mathf.Clamp01(ActionProgress.Value + Time.deltaTime / ActionDuration);
                await UniTask.NextFrame();
            }
        }

        public void Cancel()
        {
            source?.Cancel();
            ActionProgress.Value = 0;
        }
    }
}