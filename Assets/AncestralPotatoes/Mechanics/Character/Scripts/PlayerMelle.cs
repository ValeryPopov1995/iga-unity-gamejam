using AncestralPotatoes.Scene;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace AncestralPotatoes.Character
{
    public class PlayerMelle : MonoBehaviour
    {
        [SerializeField] private InputAction strike;
        [SerializeField] private float cooldown = .9f;
        [SerializeField] private GameObject shovel;
        [SerializeField] private AudioClip strikeClip;
        [SerializeField] private float animationDelay;
        [SerializeField] private Transform triggerPoint;
        [SerializeField] private float radius = .5f;
        [SerializeField] private float damage = .5f;
        [Inject] private readonly Player player;
        [Inject] private readonly SfxPlayer sfxPlayer;
        private float time;

        private void Start()
        {
            strike.started += Strike;
            strike.Enable();
            shovel.SetActive(false);
        }

        private void OnDestroy()
        {
            strike.started -= Strike;
            strike.Disable();
        }

        private async void Strike(InputAction.CallbackContext context)
        {
            if (time + cooldown > Time.time) return;
            shovel.SetActive(true);
            sfxPlayer.PlayOneShot(strikeClip, transform.position);
            player.Animator.Strike();

            await UniTask.Delay(TimeSpan.FromSeconds(animationDelay));

            var damages = Physics.OverlapSphere(triggerPoint.position, radius)
                .Select(collider => collider.GetComponent<IDamageReceiver>())
                .Where(receiver => receiver != null && receiver != player);

            var description = new DamageDescription()
            {
                Amount = damage,
                Type = EDamageType.Impact
            };
            foreach (var damage in damages)
                damage.ReceiveDamage(description);

            time = Time.time;

            await UniTask.Delay(TimeSpan.FromSeconds(animationDelay));
            shovel.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (triggerPoint)
                Gizmos.DrawWireSphere(triggerPoint.position, radius);
        }
    }
}