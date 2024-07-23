namespace StateMachine.States
{
    using StateMachine.Interface;

    public abstract class BaseGameState : IGameState
    {
        public          GameStateMachine GameStateMachine;
        public abstract void             Enter();

        public abstract void Exit();
    }
}