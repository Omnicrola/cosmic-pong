using System;

namespace Assets.Scripts.Core
{
    public interface IGameEventBus
    {
        void Broadcast(IGameEvent eventArgs);
        void Subscribe<T>(Action<T> eventHandlerOne) where T : IGameEvent;
        void Unsubscribe<T>(Action<T> eventHandlerOne) where T : IGameEvent;
    }
}