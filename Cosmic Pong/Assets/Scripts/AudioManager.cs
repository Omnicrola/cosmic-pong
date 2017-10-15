using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip BallImpact;
        public AudioClip BallDespawn;
        public AudioClip BallSpawn;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();

            var eventBus = GameManager.Instance.EventBus;
            eventBus.Unsubscribe<BallHitPaddleEvent>(PlayImpactClip);
            eventBus.Subscribe<BallDespawnEvent>(PlayDespawnClip);
            eventBus.Subscribe<BallSpawnEvent>(PlaySpawnClip);
        }


        void OnDestroy()
        {
            var eventBus = GameManager.Instance.EventBus;
            eventBus.Unsubscribe<BallHitPaddleEvent>(PlayImpactClip);
            eventBus.Unsubscribe<BallDespawnEvent>(PlayDespawnClip);
            eventBus.Unsubscribe<BallSpawnEvent>(PlaySpawnClip);
        }

        private void PlayImpactClip(BallHitPaddleEvent obj)
        {
            PlayClip(BallImpact);
        }

        private void PlayDespawnClip(BallDespawnEvent obj)
        {
            PlayClip(BallDespawn);
        }

        private void PlaySpawnClip(BallSpawnEvent obj)
        {
            PlayClip(BallSpawn);
        }

        private void PlayClip(AudioClip clipToPlay)
        {
            _audioSource.clip = clipToPlay;
            _audioSource.time = 0;
            _audioSource.Play();
        }
    }
}