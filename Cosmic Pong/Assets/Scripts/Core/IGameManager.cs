namespace Assets.Scripts.Core
{
    public interface IGameManager
    {
        IGameEventBus EventBus { get; }
        bool Pause { get; set; }
        string TransitionToScene { get; set; }
        CoroutineBuilder After(int milliseconds);
    }

}