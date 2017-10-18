using Assets.Scripts.Core;
using Assets.Scripts.Core.Events;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallController : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        void OnCollisionEnter(Collision collision)
        {
            var tagCollidingWith = collision.gameObject.tag;
            if (tagCollidingWith == Tags.PONG_OUT_OF_BOUNDS)
            {
                Broadcast(new BallOutOfBoundsEvent());
            }
            else if (tagCollidingWith == Tags.PONG_FLOOR)
            {
                Broadcast(new BallOutOfBoundsEvent());
            }
            else if (tagCollidingWith == Tags.PONG_NET)
            {
                Broadcast(new BallHitNetEvent());
            }

            else if (tagCollidingWith == Tags.PONG_PADDLE)
            {
                Broadcast(new BallHitPaddleEvent());
            }
        }

        private static void Broadcast(IGameEvent eventToBroadcast)
        {
            GameManager.Instance.EventBus.Broadcast(eventToBroadcast);
        }
    }
}