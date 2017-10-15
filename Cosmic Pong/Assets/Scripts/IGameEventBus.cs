using System;
using Assets.Scripts.Events;

namespace Assets.Scripts
{
    public interface IGameEventBus
    {
        void Broadcast(IGameEvent eventArgs);
        void Subscribe<T>(Action<T> eventHandlerOne) where T : IGameEvent;
        void Unsubscribe<T>(Action<T> eventHandlerOne) where T : IGameEvent;
    }
}