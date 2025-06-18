using AncestralPotatoes.Character;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace AncestralPotatoes.Scene
{
    public class ScenarioIntro : MonoBehaviour
    {
        [SerializeField] private double delayToSpawb;
        [SerializeField] private double delayToDestroy;
        [Inject] private readonly Player player;
        [Inject] private readonly PlayerCamera PlayerCamera;

        private async void Start()
        {
            player.gameObject.SetActive(false);
            PlayerCamera.gameObject.SetActive(false);
            await UniTask.Delay(TimeSpan.FromSeconds(delayToSpawb));
            player.gameObject.SetActive(true);
            PlayerCamera.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(delayToDestroy));
            Destroy(gameObject);
        }
    }
}