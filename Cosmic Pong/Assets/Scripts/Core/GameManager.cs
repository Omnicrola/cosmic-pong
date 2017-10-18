using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Core
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

        public CoroutineBuilder After(int milliseconds)
        {
            return new CoroutineBuilder(milliseconds, this);
        }

        protected void Start()
        {
        }

        IEnumerator ExecuteCoroutineBuilder(CoroutineBuilder coroutineBuilder)
        {
            yield return new WaitForSeconds(coroutineBuilder.Milliseconds / 1000f);
            coroutineBuilder.ActionToRun();
        }
    }

    public class CoroutineBuilder
    {
        public int Milliseconds { get; private set; }
        private readonly MonoBehaviour _monoBehaviour;
        public Action ActionToRun { get; private set; }

        public CoroutineBuilder(int milliseconds, MonoBehaviour monoBehaviour)
        {
            Milliseconds = milliseconds;
            _monoBehaviour = monoBehaviour;
        }

        public void Then(Action action)
        {
            ActionToRun = action;
            _monoBehaviour.StartCoroutine("ExecuteCoroutineBuilder", this);
        }
    }
}