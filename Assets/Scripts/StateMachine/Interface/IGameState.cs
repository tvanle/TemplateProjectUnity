namespace StateMachine.Interface
{
    using tvan.uni.foundation.UniCore.StateMachine.Interface;

    public interface IGameState : IState
    {
    }

    public interface IGameState<T> : IGameState
    {
        T Model { get; set; }
    }
}