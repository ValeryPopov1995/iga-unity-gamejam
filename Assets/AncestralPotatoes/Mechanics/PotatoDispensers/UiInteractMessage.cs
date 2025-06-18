using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AncestralPotatoes.PotatoDispancers
{
    public class UiInteractMessage : MonoBehaviour
    {
        private float load01
        {
            get => bar.fillAmount;
            set => bar.fillAmount = value;
        }

        private float alpha
        {
            get => canvasGroup.alpha;
            set => canvasGroup.alpha = value;
        }

        [SerializeField] private Interaction interaction;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float startAlpha = .5f;
        [SerializeField] private float fadeDuration = .5f;
        [SerializeField] private Image bar;
        [SerializeField] private TMP_Text label;
        [SerializeField] private TMP_Text description;
        private IDisposable subscription;
        private CancellationTokenSource source;

        private void Start()
        {
            interaction.OnEnter += Show;
            interaction.OnExit += Hide;
            alpha = 0;
        }

        private void OnDestroy()
        {
            interaction.OnEnter -= Show;
            interaction.OnExit -= Hide;
        }

        private async void Show(Interactable interactable)
        {
            subscription?.Dispose();
            subscription = interactable.InteractAction.ActionProgress.Subscribe(UpdateBar);

            label.text = interactable.Name;
            description.text = interactable.ActionDesription;

            var token = ResetCancellation();
            while (alpha < startAlpha && !token.IsCancellationRequested)
            {
                alpha += Time.deltaTime / fadeDuration;
                await UniTask.NextFrame();
            }
        }

        private async void Hide(Interactable interactable)
        {
            label.text = "";
            description.text = "";
            subscription.Dispose();

            var token = ResetCancellation();
            while (alpha > 0 && !token.IsCancellationRequested)
            {
                alpha -= Time.deltaTime / fadeDuration;
                await UniTask.NextFrame();
            }
        }

        private void UpdateBar(float progress)
        {
            load01 = progress;
        }

        private CancellationToken ResetCancellation()
        {
            source?.Cancel();
            source = new();
            return source.Token;
        }
    }
}