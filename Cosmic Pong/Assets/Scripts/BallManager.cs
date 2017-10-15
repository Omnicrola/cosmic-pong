using System.Collections;
using Assets.Scripts.Events;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallManager : MonoBehaviour
    {
        public GameObject RespawnTarget;
        public GameObject Ball;

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
            ExecuteAfterTime(5);
        }

        IEnumerator ExecuteAfterTime(float time)
        {
            Debug.Log("Respawning in " + time + " seconds");
            yield return new WaitForSeconds(time);

            Debug.Log("Respawn!");
            Ball.SetActive(true);
            Ball.transform.position = RespawnTarget.transform.position;
            GameManager.Instance.EventBus.Broadcast(new BallSpawnEvent());
        }

        void Update()
        {
        }
    }
}