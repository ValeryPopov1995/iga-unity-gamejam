using Cysharp.Threading.Tasks;
using System.Threading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AncestralPotatoes.Character.Ui
{
    public class UiThrowBar : MonoBehaviour
    {
        private float barSize01
        {
            get => bar.rectTransform.sizeDelta.x / loadSize;
            set => bar.rectTransform.sizeDelta = new(loadSize * value, bar.rectTransform.sizeDelta.y);
        }

        private float alpha
        {
            get => canvasGroup.alpha;
            set => canvasGroup.alpha = value;
        }

        [SerializeField] private Image bar;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float startAlpha = .5f;
        [SerializeField] private float fadeDuration = 1;
        [Inject] private readonly Player player;
        private CancellationTokenSource source;
        private float loadSize;

        private void Start()
        {
            loadSize = bar.rectTransform.sizeDelta.x;
            player.Hand.ThrowLoad01.Subscribe(UpdateBar);
            alpha = 0;
        }

        private void OnDestroy()
        {
            source?.Cancel();
        }

        private void UpdateBar(float load01)
        {
            barSize01 = load01;

            if (load01 == 0)
            {
                source = new();
                FadeBar(fadeDuration, source.Token);
                return;
            }

            if (!source.IsCancellationRequested)
                source?.Cancel();

            if (alpha < startAlpha)
                alpha = startAlpha;
        }

        private async void FadeBar(float fadeDuration, CancellationToken token)
        {
            var load = startAlpha;
            while (load > 0 && !token.IsCancellationRequested)
            {
                alpha -= Time.deltaTime / fadeDuration;
                await UniTask.NextFrame();
            }
        }
    }
}