using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        #region Singleton
        private static IGameManager _instance = null;

        public static IGameManager Instance
        {
            get { return _instance; }
        }


        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }


        public GameManager()
        {
            EventBus = new UniversalEventBus();

        }
        #endregion

        protected void Update()
        {
        }

        public string TransitionToScene { get; set; }


        public IGameEventBus EventBus { get; private set; }

        public bool Pause { get; set; }

        protected void Start()
        {
        }

    }
}