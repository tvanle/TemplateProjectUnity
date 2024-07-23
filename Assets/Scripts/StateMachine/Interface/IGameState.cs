namespace StateMachine.Interface
{
    using GDK_TrongLe.UniCore.StateMachine.Interface;

    public interface IGameState : IState
    {
    }

    public interface IGameState<T> : IGameState
    {
        T Model { get; set; }
    }
}