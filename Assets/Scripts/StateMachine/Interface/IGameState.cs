namespace StateMachine.Interface
{
    using Tvan.Foundation.UniCore.StateMachine.Interface;

    public interface IGameState : IState
    {
    }

    public interface IGameState<T> : IGameState
    {
        T Model { get; set; }
    }
}