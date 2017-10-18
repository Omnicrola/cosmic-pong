using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts
{
    public class OpponentPaddleController : MonoBehaviour
    {
        public GameObject Ball;
        public Vector3 SwingForce;
        public float MinX = -1;
        public float MinY = -1;
        public float MaxX = 1;
        public float MaxY = 1;

        private Rigidbody _rigidBody;
        private Vector3 _startingPlane;

        void Start()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _startingPlane = transform.position;
        }

        void Update()
        {
            var currentPosition = transform.position;
            var ballPosition = Ball.transform.position;
            var newPosition = new Vector3(ballPosition.x, ballPosition.y, currentPosition.z);

            newPosition.x = Mathf.Min(newPosition.x, MaxX);
            newPosition.x = Mathf.Max(newPosition.x, MinX);
            newPosition.y = Mathf.Min(newPosition.y, MaxX);
            newPosition.y = Mathf.Max(newPosition.y, MaxY);

            transform.position = newPosition;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == Tags.PONG_BALL)
            {
                _rigidBody.AddForce(SwingForce);
                GameManager.Instance.After(1000).Then(MovePaddBackToStartingPlane);
            }
        }

        private void MovePaddBackToStartingPlane()
        {
            var currentPosition = transform.position;
            transform.position = new Vector3(currentPosition.x, currentPosition.y, _startingPlane.z);
        }
    }
}