using UnityEngine;

namespace AncestralPotatoes.Scene
{
    public class SfxPlayer
    {
        private AudioSource audioSource;

        public static SfxPlayer Create()
        {
            var player = new GameObject("Sfx Player (one shot)");
            var source = player.AddComponent<AudioSource>();
            return new SfxPlayer() { audioSource = source };
        }

        public void PlayOneShot(AudioClip clip, Vector3 position = default, float volume = 1)
        {
            if (Time.time < 1) return;
            if (clip == null) return;
            audioSource.spatialBlend = position == default ? 0 : 1;
            audioSource.transform.position = position;
            audioSource.PlayOneShot(clip, volume);
        }
    }
}