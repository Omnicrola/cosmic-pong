using System.Collections;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallManager : MonoBehaviour
    {
        public GameObject RespawnTarget;
        public GameObject Ball;
        public float RespawnTime = 5;

        void Start()
        {
            var eventBus = GameManager.Instance.EventBus;
            eventBus.Subscribe<BallHitNetEvent>(DespawnBall);
            eventBus.Subscribe<BallOutOfBoundsEvent>(DespawnBall);
        }

        void OnDestroy()
        {
            var eventBus = GameManager.Instance.EventBus;
            eventBus.Unsubscribe<BallHitNetEvent>(DespawnBall);
            eventBus.Unsubscribe<BallOutOfBoundsEvent>(DespawnBall);
        }

        private void DespawnBall(IGameEvent obj)
        {
            Ball.SetActive(false);
            GameManager.Instance.EventBus.Broadcast(new BallDespawnEvent());
            GameManager.Instance.After(5000).Then(() =>
            {
                Ball.SetActive(true);
                Ball.transform.position = RespawnTarget.transform.position;
                GameManager.Instance.EventBus.Broadcast(new BallSpawnEvent());
            });

            //            StartCoroutine("RespawnAfterDelay");
        }

        IEnumerator RespawnAfterDelay()
        {
            yield return new WaitForSeconds(RespawnTime);

            Ball.SetActive(true);
            Ball.transform.position = RespawnTarget.transform.position;
            GameManager.Instance.EventBus.Broadcast(new BallSpawnEvent());
        }

        void Update()
        {
        }
    }
}