using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IGameManager
    {
        IGameEventBus EventBus { get; }
        bool Pause { get; set; }
        string TransitionToScene { get; set; }
    }
}